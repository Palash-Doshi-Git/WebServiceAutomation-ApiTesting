﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceAutomation.Authentication
{
    public class Base64StringConvertor
    { 
        public static string GetBase64String(string username, string password)
        {
            string auth = username + ":" + password;
            byte[] authArray =System.Text.UTF8Encoding.UTF8.GetBytes(auth);
            return System.Convert.ToBase64String(authArray);
        }


    }
}
