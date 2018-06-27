using System;
using System.ComponentModel.DataAnnotations;
using NewBelt.Models;
using System.Collections.Generic;
public class Post 

    {
        public int PostId{get;set;}
        public int UsersId{get;set;}
        public Users Users{get;set;}
        [Required]
        public string Posts{get;set;}
        public int Likes{get;set;}
        public string AliasName{get;set;}
        public List <Likers> PostThatAreLiked { get; set; }
        public int numParticipants {get;set;} = 0; 
        public Post()
        {
            PostThatAreLiked = new List<Likers>();
        }

    }
