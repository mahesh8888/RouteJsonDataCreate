namespace RouteJsonDataCreate.Models
{
    public class Settings
    {
        public string RoadsFilePath { get; set; } = string.Empty;
        public string SequenceType { get; set; } = "Standard";

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Facility
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public double Duration { get; set; } = 0.33;
        public string Material { get; set; } = "Waste";
        public List<string> Commodities { get; set; }
        public List<object> ContainerTypes { get; set; } = [];
        public List<object> Containers { get; set; } = [];
    }

    
    public class Model
    {
        public List<Facility> Facilities { get; set; }
        public List<Routes> Routes { get; set; }
    }

    public class RouteJsonModel
    {
        public Settings Settings { get; set; }
        public Model Model { get; set; }
    }
}
