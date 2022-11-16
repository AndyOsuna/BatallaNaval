using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatallaNaval.Persistence
{
    class pLogin
    {
        public static bool AuthLogin(String User, String Password)
        {
            bool conf=true;
            MessageBox.Show($"{User} {Password}");
            return conf;
        }
    }
}
