namespace SkyWheels.Contact.API.Dtos
{
    public class ResultContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
