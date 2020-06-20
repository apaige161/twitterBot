using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TInvi
{
    class Sensitive
    {

        //need to fix this, causing an exception bc program cannot find file (set up ref?)
        private static string pathOfHelperProgram = @".\talkToTInvi.exe";
        //sets the path so it is easier to work with
        public static string Helper
        {
            get { return pathOfHelperProgram; }
            set { pathOfHelperProgram = value; }
        }





        /*****currently not using anything under here*****/
        //login keys

        private static string api_key;
        private static string api_key_secret;
        private static string access_token;
        static string access_token_secret;

        //private static string api_key = "uTj4unXn3hz7JHSoM4chDYTTJ";
        public static string Apikey
        {
            get { return api_key; }
            set { api_key = value; }
        }

        //private static string api_key_secret = "xdgYIenHNbmMc3KDtZI9ESpSTBGvg8tHuvEIFDl67yEIzokOE0";
        public static string ApikeySecret
        {
            get { return api_key_secret; }
            set { api_key_secret = value; }
        }

        //private static string access_token = "1263248219158589448-auEYNsVYLwsnfggstfr5RC6u7zv4Ao";
        public static string Accesstoken
        {
            get { return access_token; }
            set { access_token = value; }
        }

        // static string access_token_secret = "QBchr66SzM63oZa1OcXHphOaHohybm4pWpMWB6XENc0zf";
        public static string AccesstokenSecret
        {
            get { return access_token_secret; }
            set { access_token_secret = value; }
        }



    }
}
