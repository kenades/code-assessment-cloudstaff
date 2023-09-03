namespace ContactApiCS.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MyDbContext _context;
        private IContactsRepository _contactsRepository;

        public RepositoryWrapper(MyDbContext context)
        {
            _context = context;
        }

        public IContactsRepository Contacts
        {
            get
            {
                if (_contactsRepository == null)
                {
                    _contactsRepository = new ContactsRepository(_context);
                }
                return _contactsRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
