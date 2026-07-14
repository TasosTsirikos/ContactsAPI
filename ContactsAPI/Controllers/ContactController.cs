using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<IActionResult> Create(CreateContactDTO dto)
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
                return Ok(contact);
            }
        }
    }
}
