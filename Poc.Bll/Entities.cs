using Poc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Bll
{
    public static class Entities
    {
        public static UnitOfWork UOW { get { return new UnitOfWork(new System.Data.Entity.DbContext("name=POCEntities")); }  }
       
    }
}
