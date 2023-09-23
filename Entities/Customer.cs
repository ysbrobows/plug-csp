namespace PlugApi.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        public string? CustomerName { get; set; }
        public string? ApiKey { get; set; }
        public bool IsActive { get; set; }
        public int? InstanceDatabaseId { get; set; }
        public string? SchemaName { get; set; }
    }
}
