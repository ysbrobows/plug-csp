namespace PlugApi.Models.Responses.Boards;

public class GetAllBoardsResponse
{
    public int maxResults { get; set; }
    public int startAt { get; set; }
    public int total { get; set; }
    public bool isLast { get; set; }
    public List<Value>? values { get; set; }

    public class Location
    {
        public int projectId { get; set; }
        public string? displayName { get; set; }
        public string? projectName { get; set; }
        public string? projectKey { get; set; }
        public string? projectTypeKey { get; set; }
        public string? avatarURI { get; set; }
        public string? name { get; set; }
    }    

    public class Value
    {
        public int id { get; set; }
        public string? self { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public Location? location { get; set; }
    }
}
