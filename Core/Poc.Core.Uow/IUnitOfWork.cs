// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUoW.cs" >
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Poc.Core.Uow
{
    using Poc.Core.Entity;
    using Poc.Core.Repository;
    #region

    using System;
    using System.Collections;
    using System.Data.SqlClient;

    #endregion

  
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="userId">
        /// The user identifier.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int SaveChanges();

        IRepository<T> Repository<T>() where T : BaseEntity;
    }
}