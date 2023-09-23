namespace PlugApi.Models.Requests.Customers
{
    public class UpdateCustomerRequest
    {
        public string? Name { get; set; }
        public Guid Api_Key { get; set; }
        public bool IsActive { get; set; }
    }
}
