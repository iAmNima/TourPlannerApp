using System;
using System.Collections.Generic;
using System.Text;

namespace TourPlannerApp.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Lr
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class Ul
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class BoundingBox
    {
        public Lr lr { get; set; }
        public Ul ul { get; set; }
    }

    public class Route
    {
        public BoundingBox boundingBox { get; set; }
        public double distance { get; set; }
        public string sessionId { get; set; }
    }

    public class Root
    {
        public Route route { get; set; }
    }
}
