using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NewBelt.Models;

namespace NewBelt.Models

{
    public class Users 

    {
        
        public int UsersId{get;set;}
        
        public string Name{get;set;} 
        
        public string Alias{get;set;} 
        
        public string Email{get;set;}
        public List <Likers> Maker { get; set; }
        public string Password{get;set;} 
        
        public Users()
        {
            Maker = new List<Likers>();
        }

    }
}