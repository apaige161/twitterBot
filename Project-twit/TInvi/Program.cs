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
        //schedule tasks

        //search pictures by keyword


        //@autoBot04768645 twitter account

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


            //program options
            string[] UserOptions = { "Text Only Tweet", "Picture and text", "Schedule Tweet for later", "Schedule picture for later" };
            int index = 1;
            Console.WriteLine("Choose an option by typing the number");
            for (int i = 0; i < UserOptions.Length; i++)
            {
                Console.WriteLine(index + "). " + UserOptions[i]);
                index++;
            }

            //get user input
            string userInput = Console.ReadLine();

            if(userInput == "1")
            {
                /************************publish the Tweet "text" on your Timeline**************************/
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What would you like to say on twitter?");
                Console.ResetColor();
                string textToTweet = Console.ReadLine();
                Tweet.PublishTweet(textToTweet + " " + DateTime.Now);
            }
            else if(userInput == "2")
            {
                /**************************publish media with a caption*******************************/

                /******promt user to pick a picture******/
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Select the number of the picture you want to post");
                Console.WriteLine("\n");
                

                /******working with the picture files******/
                //set color of files
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                //file name of all pictures
                string pathOfPics = @"C:\Users\apaig\Documents\VSRepo\Project-twit\TInvi\twitterImg";
                //puts each file into the array
                string[] files = Directory.GetFiles(pathOfPics);

                //find file name and add to list
                List<string> fileNames = new List<string>();
                for (int iFile = 0; iFile < files.Length; iFile++)
                {
                    //grabs each file name
                    string fn = new FileInfo(files[iFile]).Name;
                    //and adds it to the list
                    fileNames.Add(fn);
                }



                //write file names to console
                for (int i = 0; i < fileNames.Count; i++)
                {
                    Console.WriteLine(i + 1 + ") " + fileNames[i]);
                }
                Console.ResetColor();



                /******search picture files******/

                //add user input into for loop to write all file containing search word
                Console.WriteLine("Choose from a list of photos or search for a keyword in the filename");
                //user input: search by keyword
                string userSearch = Console.ReadLine();

                if (userSearch != null)
                {
                    for (int i = 0; i < fileNames.Count; i++)
                    {
                        //searches for a keyword
                        bool searchedFileNames = fileNames[i].Contains(userSearch);
                        if(searchedFileNames == true)
                        {
                            //add a new list or array to hold results
                            //if no results then print a message
                            //prints out files containing that keyword
                            Console.WriteLine(i + 1 + ") " + fileNames[i]);
                        }
                        
                    }
                    Console.WriteLine("end of results");
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Select the number of the picture you want to post");
                Console.WriteLine("\n");
                Console.ResetColor();


                //user input : picture number
                string userChoicePicture = Console.ReadLine();
                //convert string to int && -1 to grab actual index
                int realUserChoice = Convert.ToInt32(userChoicePicture) - 1;
                
                //full path of a file selected
                string filePath = pathOfPics + @"\" + fileNames[realUserChoice].ToString();
                
                //user chooses picture caption
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What would you like to say with your picture on twitter?");
                Console.ResetColor();
                //capture input
                string textToTweet = Console.ReadLine();

                /**************************send tweet*******************************/
                //exception unhandled if no input
                byte[] file1 = File.ReadAllBytes(filePath);
                var media = Upload.UploadBinary(file1);
                Tweet.PublishTweet(textToTweet + " " + DateTime.Now, new PublishTweetOptionalParameters
                {
                    Medias = new List<IMedia> { media }
                });
            }
            else if (userInput == "3")
            {
                /**************************schedule a tweet*******************************/
            }
            else if (userInput == "4")
            {
                /**************************schedule a picture*******************************/
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
