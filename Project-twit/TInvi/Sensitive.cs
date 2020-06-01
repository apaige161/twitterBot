using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TInvi
{
    class Sensitive
    {
        //login keys
        private static string api_key = "uTj4unXn3hz7JHSoM4chDYTTJ";
        public static string apikey
        {
            get { return api_key; }
            set { api_key = value; }
        }

        private static string api_key_secret = "xdgYIenHNbmMc3KDtZI9ESpSTBGvg8tHuvEIFDl67yEIzokOE0";
        public static string apikeySecret
        {
            get { return api_key_secret; }
            set { api_key_secret = value; }
        }

        private static string access_token = "1263248219158589448-auEYNsVYLwsnfggstfr5RC6u7zv4Ao";
        public static string accesstoken
        {
            get { return access_token; }
            set { access_token = value; }
        }

        private static string access_token_secret = "QBchr66SzM63oZa1OcXHphOaHohybm4pWpMWB6XENc0zf";
        public static string accesstokenSecret
        {
            get { return access_token_secret; }
            set { access_token_secret = value; }
        }



    }
}
