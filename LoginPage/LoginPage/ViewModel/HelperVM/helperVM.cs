using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using DocuSign.eSign.Model;
using LoginPage.Model;
namespace LoginPage.ViewModel.HelperVM
{
    public class helperVM
    {
        bool validLogin(string username, string password)
        {
            //if(username!= firstName || password != lastName) return false;
            //else if(username== firstName && password !=lastName) return false;
            //else if(username!=firstName && password==lastName) return false;
            return true;
        }
    }
}
