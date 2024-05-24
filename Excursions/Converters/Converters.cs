using Excursions.DataTransferObject;

namespace Excursions.Converters
{
    public static class Converters
    {
        private static OrganizerDTO ToDTO(this Organizer organizer)
        {
            return new OrganizerDTO
            {
                FirstName = organizer.FirstName,
                LastName = organizer.LastName
            };
        }

        private static Organizer FromDTO(this OrganizerDTO dto)
        {
            return new Organizer(dto.FirstName!, dto.LastName!);
        }

        private static ExcursionDTO ToDTO(this Excursion excursion)
        {
            return new ExcursionDTO
            {
                Organizer = excursion.Organizer.ToDTO(),
                FormOfConduct = excursion.FormOfConduct,
                Price = excursion.Price,
                Location = excursion.Location,
                Date = excursion.Date
            };
        }

        private static Excursion FromDTO(this ExcursionDTO dto)
        {
            return new Excursion(
                dto.Organizer!.FromDTO(),
                dto.FormOfConduct,
                dto.Price,
                dto.Location,
                dto.Date
            );
        }

        public static TourDTO ToDTO(this Tour tour)
        {
            return new TourDTO
            {
                StartDate = tour.StartDate,
                Excursions = tour.Excursions.Select(e => e.ToDTO()).ToList()
            };
        }

        public static Tour FromDTO(this TourDTO dto)
        {
            Tour tour = new Tour(dto.StartDate);
            tour.Excursions.AddRange(dto.Excursions!.Select(e => e.FromDTO()));
            return tour;
        }
    }
}