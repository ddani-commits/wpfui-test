using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using UiDesktopApp1.ViewModels.Category;

namespace UiDesktopApp1.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ParentCategory { get; set; }

      
        public Category( string name, string parentCategory)
        {          
            Name = name;
            ParentCategory = parentCategory;
        }


        //[NotMapped]
        //public CategoryViewModel ViewModel { get; internal set; }

    }
}
