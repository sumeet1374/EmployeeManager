using System;

namespace Common.Data
{
    /// <summary>
    ///  Interface for generic unit of work
    ///  This abstraction is created so that we can use data layer with any ORM provider
    ///  rather than just Entity Framework
    /// </summary>
    public interface IUnitOfWork:IDisposable
    {
        T GetContext<T>() where T : class;
        void Commit();
    }
}
