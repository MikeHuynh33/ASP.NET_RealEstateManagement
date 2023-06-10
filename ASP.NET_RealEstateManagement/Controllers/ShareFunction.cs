using ASP.NET_RealEstateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ShareFunction : System.Web.Mvc.Controller
{
    public static Boolean FindAdmin( string userName) {

        List<User> listOfAdmin = new List<User>
                {
                    new User { username = "minh160394@gmail.com"},
                    new User { username = "minh160394@demo.com"}
                };
        if (userName != "")
        {
           
            if (listOfAdmin.Any(admin => admin.username == userName))
            {

                return true;
            }
            else
            {
                return false;
            };
        }
        return false;
    }
}