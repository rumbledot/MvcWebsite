using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcWebsite.Models
{
    public class BoardColorSelectorViewModel
    {
        private const string PEG_WHITE = "white";
        private const string PEG_YELLOW = "yellow";
        private const string PEG_GREEN = "green";
        private const string PEG_BLUE = "blue";
        private const string PEG_PINK = "pink";

        public Board NewBoard { get; set; }
        public string selectedBoardColor { get; set; }
        public SelectList BoardColors;

        public BoardColorSelectorViewModel() {
            BoardColors = new SelectList (
                new List<SelectListItem> {
                    new SelectListItem { Selected = true, Text="White",  Value = PEG_WHITE },
                    new SelectListItem { Text="Yellow", Value = PEG_YELLOW },
                    new SelectListItem { Text="Blue",   Value = PEG_BLUE },
                    new SelectListItem { Text="Green",  Value = PEG_GREEN },
                    new SelectListItem { Text="Pink",   Value = PEG_PINK },
                }, "Value", "Text", 1);
        }
    }
}