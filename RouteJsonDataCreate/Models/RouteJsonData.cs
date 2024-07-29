namespace RouteJsonDataCreate.Models
{
    public class RouteJsonData
    {
        //public decimal DumpLat { get; set; }
        //public decimal DumpLng { get; set; }
        public decimal YardLat { get; set; }
        public decimal YardLng { get; set; }
        public string RoadsFilePath { get; set; } 
        public string FileName { get; set; }
        public string RouteName { get; set; }
        public bool IsSuccess { get; set; } = false;
        public bool IsInfo { get; set; } = false;
    }
}
