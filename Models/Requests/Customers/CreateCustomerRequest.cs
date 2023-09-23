using PlugApi.Models.Enums;

namespace PlugApi.Models.Requests.Customers
{
    public class CreateCustomerRequest
    {
        public string? Name { get; set; }
        public Guid ApiKey { get; set; } = Guid.NewGuid();
        public DataBaseTypeEnum DataBaseType { get; set; }
    }
}
