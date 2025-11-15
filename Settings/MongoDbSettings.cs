namespace GoVisit.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string AppointmentsCollection { get; set; } = "appointments";
    }
}
