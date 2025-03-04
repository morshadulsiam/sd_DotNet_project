using System.ComponentModel.DataAnnotations.Schema;


namespace Crud.web.Models.Entities
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string PetCatagory { get; set; }
        public string PetAge { get; set; }
        public string Description { get; set; }
        public int Phone { get; set; }
        public string ImagePath { get; set; } // Store image file path

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        // New Property: Store the ID of the user who created the pet post
        public string UserId { get; set; }

        public bool IsAdopted { get; set; }

        public string? AdoptedByUserId { get; set; }
    }
}


