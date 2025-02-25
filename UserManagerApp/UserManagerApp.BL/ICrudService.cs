namespace UserManagerApp.BL
{
    using System.Collections.Generic;

    public interface ICrudService<T>
    {
        void Add(T entity);
        List<T> GetAll();
    }
}
