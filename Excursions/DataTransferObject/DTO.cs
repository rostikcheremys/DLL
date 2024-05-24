namespace Excursions.DataTransferObject;

public class OrganizerDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class ExcursionDTO
{
    public OrganizerDTO? Organizer { get; set; }
    public FormOfConduct FormOfConduct { get; set; }
    public int Price { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
}

public class TourDTO
{
    public DateTime StartDate { get; set; }
    public List<ExcursionDTO>? Excursions { get; set; }
}
