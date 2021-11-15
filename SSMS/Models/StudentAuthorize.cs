using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSMS.Models
{
    public class StudentAuthorize
    {
        SSMSDataContext context = new SSMSDataContext();
        public bool isUserStudent(string UserName)
        {
            string UserType = context.Users.SingleOrDefault(user => user.UserName == UserName).UserType;

            return (UserType == "student");
        }
    }
}