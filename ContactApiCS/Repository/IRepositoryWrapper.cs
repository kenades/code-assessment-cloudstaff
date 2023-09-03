namespace ContactApiCS.Repository;

public interface IRepositoryWrapper
{
    IContactsRepository Contacts { get; }
    void Save();
}