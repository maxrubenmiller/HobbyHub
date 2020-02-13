using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyHub.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId {get;set;}

        [Required]
        [Display(Name = "Hobby Title: ")]
        public string Title{get;set;}

        [Required]
        [Display(Name="Description: ")]
        public string Description{get;set;}

        public int UserId{get;set;}
        public List<Rsvp> GuestList{get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt{get;set;}
    }
}