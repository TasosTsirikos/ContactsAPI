using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsAPI.Data;
using ContactsAPI.DTOs;
using ContactsAPI.Models;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("ContactList")]
    // Route: /ContactList
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ContactController> _logger;

        // Constructor for Dependency Injection
        public ContactController(ILogger<ContactController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GetAllContacts
        [HttpGet("GetAllContacts")]
        public async Task<IActionResult> Get()
        {
            var contacts = await (
                from c in _context.Contacts
                select c
            ).ToListAsync();

            return Ok(contacts);
        }

        // CreateContact
        [HttpPost("CreateContact")]
        public async Task<IActionResult> Create(ContactDTO dto)
        {
            if(dto.FirstName == null || dto.LastName == null || dto.Phone == null)
            {
                return BadRequest();
            }
            else
            {
                var contact = new Contact
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Phone = dto.Phone,
                    CreatedAt = DateTime.Now
                };

                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }

        // DeleteContact
        [HttpDelete("DeleteContact/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
            
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // UpdateContact
        [HttpPut("UpdateContact/{id}")]
        public async Task<IActionResult> Update(int id, ContactDTO dto)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            if (dto.FirstName == null || dto.LastName == null || dto.Phone == null)
            {
                return BadRequest();
            }
            else
            {
                contact.FirstName = dto.FirstName;
                contact.LastName = dto.LastName;
                contact.Phone = dto.Phone;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
