using PlugApi.Models.Enums;

namespace PlugApi.Models.Requests.Customers
{
    public class CreateCustomerRequest
    {
        public string? Name { get; set; }
        public string? ApiKey { get; set; }
        public string? ProjectKey { get; set; }
        public DataBaseTypeEnum DataBaseType { get; set; }
        public IssueMapping? IssueMapping { get; set; }

    }

    public class IssueMapping
    {
        public string? OriginalEstimate { get; set; }
        public string? TimeSpent { get; set; }
        public string? creatorName { get; set; }
    }

}
