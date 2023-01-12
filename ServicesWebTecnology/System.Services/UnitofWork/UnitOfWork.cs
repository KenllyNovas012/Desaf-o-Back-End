using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemQuickzal.Contexts;

namespace System.Infrastructure.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public AplicationDataContext Context { get; }

        public UnitOfWork(AplicationDataContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
