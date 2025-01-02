using Microsoft.AspNetCore.Mvc;
using SkyWheels.Contact.API.Dtos;
using SkyWheels.Contact.API.Services;

namespace SkyWheels.Contact.API.Controllers
{
    [Route("sky-wheels-api/contact")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService contactService;

        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }


        [Route("get-all-contact")]
        [HttpGet]
        public async Task<IActionResult> GetAllContact()
        {
            var result = await contactService.GetAll();
            return Ok(result);
        }

        [Route("get-contact/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var result = await contactService.GetById(id);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result.Message);
        }

        [Route("create-contact")]
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactDto createContactDto)
        {
            var result = await contactService.Create(createContactDto);
            return Ok(result.Message);
        }

        [Route("update-contact")]
        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] UpdateContactDto updateContactDto)
        {
            var result = await contactService.Update(updateContactDto);
            return Ok(result.Message);
        }

        [Route("delete-contact/{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var result = await contactService.Delete(id);
            return Ok(result.Message);
        }
    }
}
