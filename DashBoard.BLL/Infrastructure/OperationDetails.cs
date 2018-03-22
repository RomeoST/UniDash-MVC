namespace DashBoard.BLL.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool successed, string message, string prop)
        {
            Successed = successed;
            Message = message;
            Property = prop;
        }

        public bool Successed { get; }
        public string Message { get; }
        public  string Property { get; }
    }
}
