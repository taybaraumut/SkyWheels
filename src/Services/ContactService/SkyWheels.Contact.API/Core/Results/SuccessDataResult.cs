namespace SkyWheels.Contact.API.Core.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, bool success, string message) : base(data, true, message)
        {

        }

        public SuccessDataResult(T data, string message) : base(data, true, message)
        {

        }

        public SuccessDataResult(T data) : base(data, true)
        {

        }

        public SuccessDataResult(string message) : base(default!, true, message)
        {

        }

        public SuccessDataResult(List<List<Dtos.ResultContactDto>> list) : base(default!, true)
        {

        }
    }
}
