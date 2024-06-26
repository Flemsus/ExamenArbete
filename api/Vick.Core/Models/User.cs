﻿using System.Collections.Generic;

namespace Vick.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; internal set; }


        public List<Artwork> UserArt { get; set; } = new List<Artwork>();
    }
}
