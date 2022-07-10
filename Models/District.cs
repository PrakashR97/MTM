using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thiru_Proj.Models
{
    public class District
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public bool IsActive { get; set; }

        public DateTime Insert_Date { get; set; }

        public DateTime Update_Date { get; set; }
    }
}
