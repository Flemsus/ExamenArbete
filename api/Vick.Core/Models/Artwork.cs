using System;

namespace Vick.Core.Models
{
    public class Artwork
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ArtName { get; set; }
        public int ArtCreatorId { get; set; }
        public string ArtDescription { get; set; }
        public int ArtPrice { get; set; }

    }
}