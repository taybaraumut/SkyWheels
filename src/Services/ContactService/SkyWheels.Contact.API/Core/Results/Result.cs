using IResult = SkyWheels.Contact.API.Core.Utilities.IResult;

namespace SkyWheels.Contact.API.Core.Results
{
    public abstract class Result : IResult
    {
        public Result(bool success)
        {
            Success = success;
        }
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        public bool Success { get; }
        public string Message { get; }

    }
}
