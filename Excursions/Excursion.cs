namespace Excursions
{
    public class Excursion(Organizer organizer, FormOfConduct formOfConduct, int price, string location, DateTime date) : ICloneable, IComparable<Excursion>
    {
        public Organizer Organizer { get;  set; } = organizer;
        public FormOfConduct FormOfConduct { get; set; } = formOfConduct;
        public int Price { get; set; } = price;
        public string Location { get; set; } = location;
        public DateTime Date { get; set; } = date;

        public object Clone()
        {
            return new Excursion((Organizer)Organizer.Clone(), FormOfConduct, Price, Location, Date);
        }

        public int CompareTo(Excursion? other)
        {
            if (other == null) return 1;
            return Date.CompareTo(other.Date);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Excursion other)
            {
                return Organizer.Equals(other.Organizer) && 
                       FormOfConduct == other.FormOfConduct && 
                       Price == other.Price && 
                       Location == other.Location && 
                       Date == other.Date;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Organizer, FormOfConduct, Price, Location, Date);
        }
        
        public override string ToString()
        {
            return $"{Location} - {FormOfConduct} - ${Price} - {Date:d} by {Organizer.FirstName} {Organizer.LastName}";
        }
    }
}



