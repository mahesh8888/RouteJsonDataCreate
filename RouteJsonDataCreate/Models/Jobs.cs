namespace RouteJsonDataCreate.Models
{
    public class Jobs
    {
        public string UniqueID { get; set; }
        public string JobType { get; set; } = "Pickup";
        public string RouteID { get; set; } = "205";
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Material { get; set; } = "Waste";
        public int NumberOfContainers { get; set; } = 1;
        public decimal Rate { get; set; } = 10.0M;
        public decimal Yield { get; set; } = 15.0M;
        public string StreetName { get; set; } = string.Empty;
        public bool SingleSided { get; set; } = true;
    }

    public class RouteLatLongData
    {        
        public string Route { get; set; }
        public List<JobLocation> Jobs { get; set; }
        
    }

    public class JobLocation
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }

    }
}
