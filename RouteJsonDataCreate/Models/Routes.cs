namespace RouteJsonDataCreate.Models
{
    public class Routes
    {
        public int RouteID { get; set; }
        public string VehicleType { get; set; } = "ASL_R";
        public List<Jobs> Jobs { get; set; } = new List<Jobs>();

    }
}
