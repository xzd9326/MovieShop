using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        [MaxLength(128)]
        public string? FirstName { get; set; }

        [MaxLength(128)]
        public string? LastName { get; set; }

        [MaxLength(7)]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(256)]
        public string? Email { get; set; }

        [MaxLength(1024)]
        public string? HashedPassword { get; set; }

        [MaxLength(1024)]
        public string? Salt { get; set; }

        [MaxLength(16)]
        public string? PhoneNumber { get; set; }
        public bool? TwoFactorEnabled { get; set; }

        [MaxLength(7)]
        public DateTime? LockoutEndDate { get; set; }

        [MaxLength(7)]
        public DateTime? LastLoginDateTime { get; set; }
        public bool? IsLocked { get; set; }
        public int? AccessFailedCount { get; set; }
        public string? ProfilePictureUrl { get; set; }

        // Navigation Property
        public ICollection<Review> UsersOfMovie { get; set; }
        public ICollection<Favorite> FavoriteMovies { get; set; }
        public ICollection<UserRole> UsersOfRole { get; set; }

    }
}
