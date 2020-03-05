using System;

namespace OKEX.Auto.Core.ORM.Dapper.Base
{
    public interface IContext : IDisposable
    {
        bool IsTransactionStarted { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}
