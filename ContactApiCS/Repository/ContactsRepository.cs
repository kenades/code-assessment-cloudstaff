using ContactApiCS.Models;

namespace ContactApiCS.Repository
{
    public class ContactsRepository : RepositoryBase<Contact>, IContactsRepository
    {
        public ContactsRepository(MyDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Contact> GetContacts()
        {
            return FindAll().ToList();
        }

        public Contact? GetContactById(int id)
        {
            return FindByCondition(contact => contact.Id.Equals(id)).FirstOrDefault();
        }

        public void AddContact(Contact contact) => Create(contact);

        public void UpdateContact(Contact contact) => Update(contact);
    }
}
