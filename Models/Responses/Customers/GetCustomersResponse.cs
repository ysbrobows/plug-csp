using PlugApi.Models.Enums;

namespace PlugApi.Models.Responses.Customers
{
    public class GetCustomersResponse
    {
        public string? Name { get; set; }
        public DataBaseTypeEnum? DataBaseType { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Guid ApiKey { get; set; }
        public bool IsActive { get; set; }
    }
}
