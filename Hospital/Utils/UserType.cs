using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Utils
{
    public enum UserType
    {
        MANAGER,
        DOCTOR,
        NURSE
    }

    public static class UserContext
    {
        public static UserType CurrentUserType { get; set; }
    }
}
