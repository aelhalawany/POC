using Poc.Core.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Bll
{
    internal static class Entities
    {
        public static UnitOfWork UOW { get { return new UnitOfWork("name=POCEntities"); }}
       
    }
}
