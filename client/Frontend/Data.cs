using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindows
{
    class Data
    {

       public Data ()
       {

       }
        String[,] Users = new string[2, 2] { {"user1", "pass1"} , {"username1", "pass1"} };
        
        public int CheckUser(string Username, string Password)
        {
            for(int i=0;i< Users.Length / 2;i++)
            {
                if(Users[i,0] == Username && Users[i,1] == Password)
                {
                    return i;
                }
            }

            return -1;
        }

    }

}
