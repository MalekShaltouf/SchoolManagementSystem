using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSMS.Models
{
    public class TeacherAuthoriz
    {
        SSMSDataContext context = new SSMSDataContext();
        public bool isUserTeacher(string UserName) {
            string UserType = context.Users.SingleOrDefault(user => user.UserName == UserName).UserType;

            return (UserType == "teacher");
        }
    }
}