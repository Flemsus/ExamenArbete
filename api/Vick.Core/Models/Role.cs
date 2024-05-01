namespace Vick.Core.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // TODO: Do we need/want this?
    public List<User> Users { get; set; }
}
