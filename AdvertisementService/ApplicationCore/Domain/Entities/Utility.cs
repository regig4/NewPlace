namespace ApplicationCore.Models
{
    public class Utility
    {
        public Utility(int? id, string name, decimal? cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal? Cost { get; set; }
    }
}
