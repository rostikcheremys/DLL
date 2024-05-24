namespace Excursions
{
    public class Tour(DateTime startDate) : ICloneable, IComparable<Tour>
    {
        private readonly DateTime _startDate = startDate;
        private List<Excursion> _excursions = new();

        public DateTime StartDate => _startDate;
        public List<Excursion> Excursions => _excursions;

        public void AddExcursion(Excursion excursion)
        {
            _excursions.Add(excursion);
        }

        public void RemoveExcursion(Excursion excursion)
        {
            _excursions.Remove(excursion);
        }

        public object Clone()
        {
            Tour clonedTour = new Tour(_startDate)
            {
                _excursions = new List<Excursion>(_excursions.Select(e => (Excursion)e.Clone()))
            };
            return clonedTour;
        }

        public int CompareTo(Tour? other)
        {
            if (other == null) return 1;
            return _startDate.CompareTo(other._startDate);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Tour other = (Tour)obj;
            return _startDate == other._startDate && _excursions.SequenceEqual(other._excursions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_startDate, _excursions);
        }

        public string GetFullInformation()
        {
            var excursionDetails = string.Join(", ", _excursions);
            return $"Tour starts on {_startDate.ToShortDateString()}: {excursionDetails}";
        }

        public string GetShortInformation()
        {
            int totalPrice = _excursions.Sum(e => e.Price);
            return $"Tour starts on {_startDate.ToShortDateString()} with a total price of ${totalPrice}";
        }

        public override string ToString()
        {
            return GetShortInformation();
        }
    }

}
