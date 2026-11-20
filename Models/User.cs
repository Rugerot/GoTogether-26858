using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTogether.Models
{
    internal class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
