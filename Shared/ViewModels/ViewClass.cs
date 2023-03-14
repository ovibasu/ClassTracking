using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTracking.Shared.ViewModels
{
    public class ViewClass
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string Standard { get; set; }
        public int TotalStudent { get; set; }
        public decimal MaxStudent { get; set; }
        public decimal SessionYear { get; set; }
    }
}
