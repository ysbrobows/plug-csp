namespace PlugApi.Helpers
{
    public static class ClientDatabase
    {

        public static String GetDatabaseConnectionByName(string customerName)
        {
            return $"User ID=masterapijira1@plugin-jira-pg;Password=@mysupremepassword1;Host=plugin-jira-pg.postgres.database.azure.com;Port=5432;Database={customerName};";
        }


    }
}
