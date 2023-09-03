using AutoMapper;
using ContactApiCS.Models;
using ContactApiCS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ContactApiCS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsApiController : ControllerBase
    {
        private readonly ILogger<ContactsApiController> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ContactsApiController(ILogger<ContactsApiController> logger,
            IRepositoryWrapper wrapper,
            IMapper mapper)
        {
            _logger = logger;
            _repository = wrapper;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            var contacts = _repository.Contacts.GetContacts();
            return Ok(contacts);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddContact([FromBody] ContactCreationDto? contact)
        {
            try
            {
                if (contact is null)
                {
                    _logger.LogError("Contact is null.");
                    return BadRequest("Contact is null.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid data sent from client.");
                    return BadRequest("Invalid data model");
                }

                var contactEntity = _mapper.Map<Contact>(contact);
                contactEntity.CreatedDate = DateTime.Now;
                contactEntity.LastUpdateDate = DateTime.Now;
                contactEntity.LastUpdatedBy = 1;

                _repository.Contacts.AddContact(contactEntity);
                _repository.Save();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went when adding a contact : {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] ContactUpdateDto? contact)
        {
            try
            {
                if (contact is null)
                {
                    _logger.LogError("Contact is null.");
                    return BadRequest("Contact is null.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid data sent from client.");
                    return BadRequest("Invalid data model");
                }

                var contactEntity = _repository.Contacts.GetContactById(id);

                if (contactEntity is not null)
                {
                    _mapper.Map(contact, contactEntity);
                    _repository.Contacts.UpdateContact(contactEntity);
                    _repository.Save();

                    return Ok();
                }
                else
                {
                    _logger.LogError($"Contact with id: {id}, is not found.");
                    return NotFound();
                }

            }
            catch (Exception e)
            {
                _logger.LogError($"Something went when updating a contact : {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("getcontact")]
        public IActionResult GetContact(int id)
        {
            try
            {
                var contactEntity = _repository.Contacts.GetContactById(id);
                return Ok(contactEntity);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went when updating a contact : {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("searchcontact")]
        public IActionResult SearchContacts(string search)
        {
            try
            {
                var contact = _repository.Contacts.GetContacts()
                    .Where(c=>c.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) 
                              || c.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                    || c.CompanyName.Contains(search, StringComparison.OrdinalIgnoreCase) 
                    || c.Mobile.Contains(search, StringComparison.OrdinalIgnoreCase) 
                              || c.Email.Contains(search, StringComparison.OrdinalIgnoreCase)).Select(c=>c);

                return Ok(contact);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
