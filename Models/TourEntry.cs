
namespace TourPlannerApp.Models
{
    public class TourEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }


        public TourEntry(int id, string name, string description, double distance)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Distance = distance;
        }

    }
}
