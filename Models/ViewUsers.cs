using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using NewBelt.Models;
public class ViewUsers
    {
        
        public int usersId{get;set;}
        [Required]
        [MinLength(2)]
        public string Name{get;set;} 
        [Required]
        [MinLength(2)]
        public string Alias{get;set;} 
        [Required]
        [EmailAddress]
        public string Email{get;set;} 
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password{get;set;} 
        [Compare("Password", ErrorMessage = "passwords do not match")]  
        public string ConfirmPassword{get;set;} 
    
    }