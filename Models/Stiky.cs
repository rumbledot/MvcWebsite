using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebsite.Models
{
    public class Stiky
    {
        public int Id { get; set; }

        [Display(Name = "Created at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }

        [StringLength(1000, MinimumLength = 10, ErrorMessage = "text board length is 10-255 chars")]
        [Required]
        public string Text { get; set; }
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        public Stiky()
        {
            Type = "text";
            CreatedAt = DateTime.Today;
        }
    }
}