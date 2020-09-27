using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebsite.Models
{
    public class Board
    {
        public const string PEG_WHITE = "white";
        public const string PEG_YELLOW = "yellow";
        public const string PEG_GREEN = "green";
        public const string PEG_BLUE = "blue";
        public const string PEG_PINK = "pink";

        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Created at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [StringLength(255, MinimumLength = 10)]
        [Required]
        public string Text { get; set; }

        [StringLength(60)]
        public string Tags { get; set; }

        [StringLength(6, MinimumLength = 4)]
        [Display(Name = "Pick Board's Color")]
        public string BoardColor { get; set; }
        public string ColorClass {
            get {
                return "peg-" + BoardColor;
            }
        }

        public Board() {
            BoardColor = PEG_WHITE;
            CreatedAt = DateTime.Today;
        }
    }
}