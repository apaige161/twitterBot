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
                Console.WriteLine(user);
                Console.WriteLine("Login Successful");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could Not Login, Check Credentials");
                Console.ResetColor();
            }

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
                var filePath = @"C:\Users\apaig\Pictures\TwitterPics\tree.jpg";
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What would you like to say with your picture on twitter?");
                string textToTweet = Console.ReadLine();
                Console.WriteLine(@"Enter the full file path of the picture. ex/ C:\Users\apaig\Pictures\TwitterPics\tree.jpg");
                filePath = Console.ReadLine();


                byte[] file1 = File.ReadAllBytes(filePath);
                var media = Upload.UploadBinary(file1);
                var tweet = Tweet.PublishTweet(textToTweet + " " + DateTime.Now, new PublishTweetOptionalParameters
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
