using IResult = SkyWheels.Contact.API.Core.Utilities.IResult;
using SkyWheels.Contact.API.Dtos;
using SkyWheels.Contact.API.Core.Results;
using SkyWheels.Contact.API.Core.Utilities;
using SkyWheels.Contact.API.Context;
using Dapper;
using Microsoft.AspNetCore.DataProtection;
using SkyWheels.Contact.API.Constants;

namespace SkyWheels.Contact.API.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactContext contactContext;
        private readonly IDataProtector dataProtector;
        private readonly IConfiguration configuration;
        private readonly string dataProtectorKey;

        public ContactService(IDataProtectionProvider dataProtectionProvider, ContactContext contactContext, IConfiguration configuration)
        {
            this.contactContext = contactContext;
            this.configuration = configuration;
            dataProtectorKey = configuration.GetSection("key").Key;
            dataProtector = dataProtectionProvider.CreateProtector(dataProtectorKey);


        }

        public async Task<IResult> Create(CreateContactDto createContactDto)
        {
            string query = "INSERT INTO Contacts (Name, Surname,PhoneNumber,Message) VALUES (@name, @surname, @phoneNumber,@message)";
            var parameters = new DynamicParameters();

            parameters.Add("@name", dataProtector.Protect(createContactDto.Name));
            parameters.Add("@surname", dataProtector.Protect(createContactDto.Surname));
            parameters.Add("@phoneNumber", dataProtector.Protect(createContactDto.PhoneNumber));
            parameters.Add("@message", dataProtector.Protect(createContactDto.Message));

            using (var connection = contactContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                return new SuccessResult(ResultConstant.Success);
            }
        }

        public async Task<IResult> Delete(Guid id)
        {
            string query = "Delete From Contacts Where Id=@Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            using (var connection = contactContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                return new SuccessResult(ResultConstant.Success);
            }
        }

        public async Task<IDataResult<List<ResultContactDto>>> GetAll()
        {
            string query = "Select * From Contacts";
            using (var connection = contactContext.CreateConnection())
            {
                var result = await connection.QueryAsync<ResultContactDto>(query);

                var decryptedContacts = result.Select(contacts => new ResultContactDto
                {
                    Id = contacts.Id,
                    Name = dataProtector.Unprotect(contacts.Name),
                    Surname = dataProtector.Unprotect(contacts.Surname),
                    PhoneNumber = dataProtector.Unprotect(contacts.PhoneNumber),
                    Message = dataProtector.Unprotect(contacts.Message),

                }).ToList();

                return new SuccessDataResult<List<ResultContactDto>>(decryptedContacts);
            }
        }

        public async Task<IDataResult<GetContactByIdDto>> GetById(Guid id)
        {
            string query = "Select * From Contacts Where Id = @Id;";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            using (var connection = contactContext.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<GetContactByIdDto>(query, parameters);

                if (result == null)
                    return new ErrorDataResult<GetContactByIdDto>(ResultConstant.NoDataFound);

                var decryptedContacts = new GetContactByIdDto
                {
                    Id = result!.Id,
                    Name = dataProtector.Unprotect(result.Name),
                    Surname = dataProtector.Unprotect(result.Surname),
                    PhoneNumber = dataProtector.Unprotect(result.PhoneNumber),
                    Message = dataProtector.Unprotect(result.Message),
                };


                return new SuccessDataResult<GetContactByIdDto>(decryptedContacts!);
            }
        }

        public async Task<IResult> Update(UpdateContactDto updateContactDto)
        {
            string query = "Update Contacts Set Name = @name, Surname = @surname, PhoneNumber = @phoneNumber, Message = @message Where Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@name", dataProtector.Protect(updateContactDto.Name));
            parameters.Add("@surname", dataProtector.Protect(updateContactDto.Surname));
            parameters.Add("@phoneNumber", dataProtector.Protect(updateContactDto.PhoneNumber));
            parameters.Add("@message", dataProtector.Protect(updateContactDto.Message));
            parameters.Add("@Id", updateContactDto.Id);

            using (var connection = contactContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                return new SuccessResult(ResultConstant.Success);
            }
        }
    }
}
