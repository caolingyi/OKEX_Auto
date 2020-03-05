using System;

namespace OKEX.Auto.Core.ORM.Dapper.Base
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
