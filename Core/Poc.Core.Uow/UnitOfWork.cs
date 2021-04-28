
using Poc.Core.Entity;
using Poc.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Core.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context = null;
        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(string ConnectionString)
        {
            this.context = new DbContext(ConnectionString);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context?.Dispose();
                var dbContext = this.context;
                dbContext?.Dispose();
            }
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repo = new BaseRepository<T>(context);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public int SaveChanges()
        {
            try
            {
                this.context.Configuration.ValidateOnSaveEnabled = false;
                var result = this.context.SaveChanges();
                return result;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException entityValidationException)
            {
                foreach (var entityValidationError in entityValidationException.EntityValidationErrors)
                {
                    var message = string.Empty;
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        message += $" \n Entity: {entityValidationError.Entry.Entity} \n Property: {validationError.PropertyName} \n Error: {validationError.ErrorMessage} \n ";
                    }
                    if (entityValidationError.ValidationErrors.Count > 0)
                    {
                        Exception exception = new Exception(message);
                        throw;
                    }
                }
            }

            return 0;
            
        }

        public IEnumerable<T> ExecuteReader<T>(string storedProcedureName, SqlParameter[] parameters = null)
        {
            if (parameters != null && parameters.Any())
            {
                var parameterBuilder = new StringBuilder();
                parameterBuilder.Append(string.Format("EXEC {0} ", storedProcedureName));

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].SqlDbType == SqlDbType.VarChar
                        || parameters[i].SqlDbType == SqlDbType.NVarChar
                        || parameters[i].SqlDbType == SqlDbType.Char
                        || parameters[i].SqlDbType == SqlDbType.NChar
                        || parameters[i].SqlDbType == SqlDbType.Text
                        || parameters[i].SqlDbType == SqlDbType.NText)
                    {
                        parameterBuilder.Append(string.Format("{0}='{1}'", parameters[i].ParameterName,
                            string.IsNullOrEmpty(parameters[i].Value.ToString())
                            ? string.Empty : parameters[i].Value.ToString()));
                    }
                    else if (parameters[i].SqlDbType == SqlDbType.BigInt
                       || parameters[i].SqlDbType == SqlDbType.Int
                       || parameters[i].SqlDbType == SqlDbType.TinyInt
                       || parameters[i].SqlDbType == SqlDbType.Decimal
                       || parameters[i].SqlDbType == SqlDbType.Float
                       || parameters[i].SqlDbType == SqlDbType.Money
                       || parameters[i].SqlDbType == SqlDbType.SmallInt
                       || parameters[i].SqlDbType == SqlDbType.SmallMoney)
                    {
                        parameterBuilder.Append(string.Format("{0}={1}", parameters[i].ParameterName
                            , parameters[i].Value));
                    }
                    else if (parameters[i].SqlDbType == SqlDbType.Bit)
                    {
                        parameterBuilder.Append(string.Format("{0}={1}", parameters[i].ParameterName,
                            Convert.ToBoolean(parameters[i].Value)));
                    }

                    if (i < parameters.Length - 1)
                    {
                        parameterBuilder.Append(",");
                    }
                }

                var query = parameterBuilder.ToString();
                var result = context.Database.SqlQuery<T>(query, parameters).ToList();
                return result;
            }
            else
            {
                var result = context.Database.SqlQuery<T>(string.Format("EXEC {0}", storedProcedureName)).ToList();
                return result;
            }
        }
    }
}
