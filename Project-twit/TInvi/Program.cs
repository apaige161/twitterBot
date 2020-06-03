using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace TInvi
{
    class Program
    {
        //I want to print out a list of files ( pictures ) to pick from 
        static void Main(string[] args)
        {
            Console.WriteLine("Bot started");

            // Set up your credentials (https://apps.twitter.com)
            Auth.SetUserCredentials(Sensitive.apikey, Sensitive.apikeySecret, Sensitive.accesstoken, Sensitive.accesstokenSecret);

            //Login
            var user = User.GetAuthenticatedUser();
            if ( user != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Login Successful");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could Not Login, Check Credentials");
                Console.ResetColor();
            }


            Console.WriteLine("\n");
            Console.WriteLine("\n");


            //user to choose bewteen program options
            string[] UserOptions = { "Text Only Tweet", "Picture and text" };
            int index = 1;
            Console.WriteLine("Choose an option by typing the number");
            for (int i = 0; i < UserOptions.Length; i++)
            {
                Console.WriteLine(index + "). " + UserOptions[i]);
                index++;
            }

            //get user input
            string userInput = Console.ReadLine();

            //conditionals that accept user input
            if(userInput == "1")
            {
                // Publish the Tweet "text" on your Timeline
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What would you like to say on twitter?");
                Console.ResetColor();
                string textToTweet = Console.ReadLine();
                Tweet.PublishTweet(textToTweet + " " + DateTime.Now);
            }
            else if(userInput == "2")
            {
                //publish media with a comment

                //user chooses picture caption
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What would you like to say with your picture on twitter?");
                Console.ResetColor();
                //capture input
                string textToTweet = Console.ReadLine();

                //user picks a picture
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(@"Enter the full file path of the picture. ex/ C:\Users\apaig\Pictures\TwitterPics\tree.jpg");
                Console.WriteLine("\n");
                Console.WriteLine("Choose from a list of photos");

                //print file name of all pictures
                string pathOfPics = @"C:\Users\apaig\Pictures\TwitterPics";
                string[] files = Directory.GetFiles(pathOfPics);

                for (int iFile = 0; iFile < files.Length; iFile++)
                {
                    string fn = new FileInfo(files[iFile]).Name;
                    Console.WriteLine("     " + fn);
                }
                Console.ResetColor();

                //capture input
                string filePath = Console.ReadLine();


                byte[] file1 = File.ReadAllBytes(filePath);
                var media = Upload.UploadBinary(file1);
                Tweet.PublishTweet(textToTweet + " " + DateTime.Now, new PublishTweetOptionalParameters
                {
                    Medias = new List<IMedia> { media }
                });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not an Option");

            }


            
            




            Console.WriteLine("Bot ended");

            Console.ReadLine();
        }
        

    }
}
