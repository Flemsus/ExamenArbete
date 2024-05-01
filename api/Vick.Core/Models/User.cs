namespace Vick.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // TODO: Create a PhoneNumber class, and use a list of it here? 
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }

    // TODO: How to handle shifts?
    public List<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}

