using System;
using System.ComponentModel.DataAnnotations;
using NewBelt.Models;

namespace NewBelt.Models
{
    public class Likers 

    {
        
        public int LikersId{get;set;}
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int PostId{get;set;}
        public int UsersId{get;set;}
        public Users Users{get;set;}
        public Post Post{get;set;}
        
    }
}