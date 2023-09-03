using ContactApiCS.Models;

namespace ContactApiCS.Repository;

public interface IContactsRepository : IRepositoryBase<Contact>
{
    IEnumerable<Contact> GetContacts();
    Contact? GetContactById(int id);
    void AddContact(Contact contact);
    void UpdateContact(Contact contact);
}