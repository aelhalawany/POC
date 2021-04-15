// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUoW.cs" >
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Poc.Repository
{
    #region

    using System;
    using System.Collections;
    using System.Data.SqlClient;

    #endregion

    /// <summary>
    ///     http://codereview.stackexchange.com/questions/19037/entity-framework-generic-repository-pattern
    /// </summary>
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

        IRepository<T> Repository<T>() where T : class;
    }
}