namespace Vick.Core.Models;

public class Shift
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Description { get; set; }
    public List<User> Users { get; set; }

    // Todo: This is a bit of a hack, but it works for now, will be replaced when we have figured out shifts
    public DayOfWeek Day => Start.Date.DayOfWeek;
  
}
