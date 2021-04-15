using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Repository
{
    public static class Extensions
    {
        /// <summary>
        /// The is dirty.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsDirty(this DbContext context)
        {
            // Query the change tracker entries for any adds, modifications, or deletes.
            var res = from e in context.ChangeTracker.Entries()
                      where
                          e.State.HasFlag(EntityState.Added) || e.State.HasFlag(EntityState.Modified)
                          || e.State.HasFlag(EntityState.Deleted)
                      select e;

            if (res.Any())
            {
                return true;
            }

            return false;
        }
    }
}
