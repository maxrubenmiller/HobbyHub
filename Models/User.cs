using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyHub.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name="First Name: ")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name="Last Name: ")]
        public string LastName { get; set; }

        [Required]
        [Display(Name="Username: ")]
        [MinLength(3)]
        [MaxLength(15)]
        public string Username { get; set; }

        [Required]
        [Display(Name="Password: ")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name="Confirm Password: ")]
        [Compare("Password")]
        [NotMapped]
        public string ComparePassword { get; set; }

        public List<Hobby> PlannedHobbies {get;set;}
        public List<Rsvp> GointTo{get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}