using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryCheck
{
    public class ClientChecker
    {
        public static bool ClientCheck(string ClientName, string ClientChef, string ClientChefPost, string ClientEmail, string ClientINN)
        {
            Regex regexNames = new Regex(@"^[А-Яа-я].*");
            Regex regexInts = new Regex(@"#^[0-9]+$#");
            Regex regexINN = new Regex(@"^[0-9]{10}|[0-9]{12}$");
            Regex regexEmail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (string.IsNullOrEmpty(ClientName) || string.IsNullOrEmpty(ClientChef) || string.IsNullOrEmpty(ClientChefPost) || string.IsNullOrEmpty(ClientEmail) || string.IsNullOrEmpty(ClientINN))
            {
                return false;
            }

            if (regexNames.IsMatch(ClientChef) != true || regexNames.IsMatch(ClientChefPost) != true)
            {
                return false;
            }
            if (ClientName.Any(Char.IsDigit))
                return false;
            if (regexINN.IsMatch(ClientINN) != true)
                return false;
            if (regexEmail.IsMatch(ClientEmail) != true)
                return false;

            return true;
        }
    }
}
