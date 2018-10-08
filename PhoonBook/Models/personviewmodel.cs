using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoonBook.Models
{
    public class personviewmodel
    {
        public int PersonId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string FaceBookAccountId { get; set; }
        public string LinkedInId { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdateOn { get; set; }
        public string ImagePath { get; set; }
        public string TwitterId { get; set; }
        public string EmailId { get; set; }
    }
}