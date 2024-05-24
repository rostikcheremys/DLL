namespace Excursions
{
    public class Organizer(string firstName, string lastName) : ICloneable, IComparable<Organizer>
    {
        private readonly string _firstName = firstName;
        private readonly string _lastName = lastName;

        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;

        public object Clone()
        {
            return new Organizer(_firstName, _lastName);
        }

        public int CompareTo(Organizer? other)
        {
            if (other == null) return 1;
            
            int result = String.Compare(_lastName, other._lastName, StringComparison.Ordinal);
            return result == 0 ? String.Compare(_firstName, other._firstName, StringComparison.Ordinal) : result;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            Organizer other = (Organizer)obj;
            return _firstName == other._firstName && _lastName == other._lastName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_firstName, _lastName);
        }

        public override string ToString()
        {
            return $"{_firstName} {_lastName}";
        }
    }
}





