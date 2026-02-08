

namespace ZekrHakeem_v11.IRepository
{
    public interface IServicesRepository<T> where T : class
    {
        List<T> GetAll();
        T FindBy(Guid Id);
        T FindBy(string Name);
        bool Save(T model); //add & update
        bool Delete(Guid Id);

    }
}
