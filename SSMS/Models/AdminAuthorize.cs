using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSMS.Models
{
    public class AdminAuthorize
    {
        public bool IsUserAdmin(string UserName)
        {
            SSMSDataContext context = new Models.SSMSDataContext();
            string UserType = context.Users.SingleOrDefault(u => u.UserName == UserName).UserType;

            return (UserType == "admin");
        }
    }
}