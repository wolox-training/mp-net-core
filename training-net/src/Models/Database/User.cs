using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

 namespace training_net.Models
{
    public class User : IdentityUser 
    {
        public List<Comment> Comments { get; set; }
    }
}
