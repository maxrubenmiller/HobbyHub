using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyHub.Models
{
    public class Rsvp
    {
        public int RsvpId{get;set;}
        public int UserId{get;set;}
        public int HobbyId{get;set;}
        public User Guest {get;set;}
        public Hobby Attending {get;set;}
        
    }
}