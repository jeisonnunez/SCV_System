using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BeautySolutions.View.ViewModel
{
    public class SubItem
    {
       
        public SubItem(string name, dynamic screen = null, bool? isvalid=null)
        {
            Name = name;
            Screen = screen;
            IsValid = isvalid;

        }

     
        public string Name { get; set; }
        public dynamic Screen { get;  set; }
        public bool? IsValid { get; set; }


       
    }

   
}