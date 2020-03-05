namespace OKEX.Auto.Core.ORM.Dapper.Base
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
