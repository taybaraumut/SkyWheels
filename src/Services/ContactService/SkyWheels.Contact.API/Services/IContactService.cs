using SkyWheels.Contact.API.Core.Utilities;
using SkyWheels.Contact.API.Dtos;
using IResult = SkyWheels.Contact.API.Core.Utilities.IResult;

namespace SkyWheels.Contact.API.Services
{
    public interface IContactService
    {
        public Task<IDataResult<List<ResultContactDto>>> GetAll();
        public Task<IDataResult<GetContactByIdDto>> GetById(Guid id);
        public Task<IResult> Create(CreateContactDto createContactDto);
        public Task<IResult> Delete(Guid id);
        public Task<IResult> Update(UpdateContactDto updateContactDto);
    }
}
