namespace SkyWheels.Contact.API.Core.Utilities
{
    public interface IDataResult<T> :IResult
    {
        T Data { get; }
    }
}
