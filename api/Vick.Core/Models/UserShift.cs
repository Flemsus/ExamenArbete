namespace Vick.Core.Models;

public class UserShift
{
    public Shift Shift { get; set; }
    public User User { get; set; }
    public Status Status { get; set; }
}