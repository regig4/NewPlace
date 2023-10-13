namespace ApplicationCore.Models
{
    public class Agency
    {
        public Agency(int? id, string name, string address, string information)
        {
            Id = id;
            Name = name;
            Address = address;
            Information = information;
        }


        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Information { get; set; }
    }


}
