﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    public class User
    {
        [Key]
        public int UserId
        {
            get;
            set;
        }

        [ForeignKey("RoleId")]
        public int RoleId
        {
            get;
            set;
        }

        [StringLength(50)]
        public string FirstName
        {
            get;
            set;
        }

        [StringLength(50)]
        public string LastName
        {
            get;
            set;
        }

        [StringLength(50)]
        public string EmailAddress
        {
            get;
            set;
        }


        [StringLength(50)]
        public string Password
        {
            get;
            set;
        }
    }
}
