using SystemQuickzal.Contexts;

namespace System.Infrastructure.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        AplicationDataContext Context { get; }
        void Commit();
    }

}
