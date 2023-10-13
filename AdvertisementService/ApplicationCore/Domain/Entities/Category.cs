namespace ApplicationCore.Models
{
    public class Category : IEquatable<Category>
    {
        public Category()
        {
        }

        public Category(int? id, EstateType apartmentType, PricingType pricingType)
        {
            Id = id;
            ApartmentType = apartmentType;
            PricingType = pricingType;
        }

        public int? Id { get; set; }
        public EstateType ApartmentType { get; set; }
        public PricingType PricingType { get; set; }

        public override bool Equals(object? obj)
        {
            Category? other = obj as Category;
            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        public bool Equals(Category other)
        {
            return Id == other.Id;
        }
    }
}
