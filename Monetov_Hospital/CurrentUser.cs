using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monetov_Hospital
{
    public static class CurrentUser
    {
        public static int UserId { get; set; }
        public static string Login { get; set; }
        public static int RoleId { get; set; }
        public static string RoleName { get; set; }
    }
}
