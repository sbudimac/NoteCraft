namespace NoteCraftAPI.Settings
{
    public class NoteCraftDatabaseSettings : INotecraftDatabaseSettings
    {
        public string UsersCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string NotesCollectionName { get; set; } = string.Empty;
    }
}
