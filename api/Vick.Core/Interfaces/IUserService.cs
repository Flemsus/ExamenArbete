using Vick.Core.Models;
using System.Collections.Generic;

namespace Vick.Core.Interfaces
{
    public interface IUserService
    {
        User GetUser();
        User GetUser(int id);
        void UpdateUser(User user);
        void DeleteUser(int id);
        User CreateUser(string name, string email, string password);
        List<User> GetAllUsers();
        User GetUserByEmail(string email);

        // Artwork related methods
        List<Artwork> GetAllArtworks();
        Artwork AddArtwork(Artwork artwork);

        bool DeleteArtwork(int artworkId);
        bool UpdateArtwork(Artwork artwork);
    }
}
