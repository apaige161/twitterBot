using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace TInvi
{
    class Program
    {
        //schedule tasks
            //calculate a new time -done
            //post a new tweet on that new time


        //@autoBot04768645 twitter account

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
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
            string[] UserOptions = { "Text Only Tweet", "Picture and Text Tweet", "Schedule Tweet for later"};
            int index = 1;
            Console.WriteLine("Choose an option by typing the number");
            for (int i = 0; i < UserOptions.Length; i++)
            {
                Console.WriteLine(index + "). " + UserOptions[i]);
                index++;
            }
            Console.WriteLine("\n");

            //get user input
            string userInput = Console.ReadLine();
            Console.WriteLine("\n");

            if (userInput == "1")
            {
                /************************publish the Tweet "text" on your Timeline**************************/
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What would you like to say on twitter?");
                Console.ResetColor();
                string textToTweet = Console.ReadLine();
                Tweet.PublishTweet(textToTweet + " " + DateTime.Now);
            }   //tweet

            else if(userInput == "2")
            {
                /**************************publish media with a caption*******************************/

                /******promt user to pick a picture******/
                Console.Write("INSTRUCTIONS: ");
                
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Select the picture you want to post");
                Console.WriteLine("\n");



                /******working with the picture files******/
                //must be in .jpg

                /******file name of all pictures******/
                string pathOfPics = @"C:\Users\apaig\Documents\VSRepo\Project-twit\TInvi\twitterImg";
                //gets each file in directory
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


                //ask user if they want to see the list of available pictures
                Console.Write("Would you like to see the available picture files to choose from?");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" Select: YES or NO");
                string showAllPictureNames = Console.ReadLine();
                Console.WriteLine("\n");

                if(showAllPictureNames == "yes" || showAllPictureNames == "" || showAllPictureNames == "y")
                {
                    //set color of files
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;

                    //write file names to console
                    for (int i = 0; i < fileNames.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ") " + fileNames[i]);
                    }
                    Console.ResetColor();
                    Console.WriteLine("\n");
                }




                /******search picture files******/
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Would you like to search for a file by name?");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" Select: YES or NO");
                string userSearch = Console.ReadLine().ToLower();
                Console.WriteLine("\n");

                //user input : search YES or NO
                if (userSearch == "yes" || userSearch == "" || userSearch == "y")
                {
                    //added user input into for loop to write all file containing search word
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Search for a picture file");
                    //user input: search by keyword
                    userSearch = Console.ReadLine();
                    Console.WriteLine("\n");
                    List<string> searchedFiles = new List<string>();
                    if (userSearch != "")
                    {
                        //color
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        for (int i = 0; i < fileNames.Count; i++)
                        {
                            //searches for a keyword
                            bool searchedFileNames = fileNames[i].Contains(userSearch);
                            if (searchedFileNames == true)
                            {
                                //list to hold results
                                searchedFiles.Add(fileNames[i]);
                                //prints out files containing that keyword
                                Console.WriteLine(i + 1 + ") " + fileNames[i]);
                            }
                            Console.ResetColor();
                        }

                        //if search doesnt result anything
                        if (!searchedFiles.Any())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("no results were found");
                            Console.ResetColor();
                            Console.WriteLine("\n");
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n");
                        Console.WriteLine("End of results");
                        Console.ResetColor();
                        Console.WriteLine("\n");
                    }
                }


                /******select picture files******/
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Select the picture by entering the number to the left of it");
                
                Console.ResetColor();


                //user input : picture number
                string userChoicePicture = Console.ReadLine();
                //convert string to int && -1 to grab actual index
                int realUserChoice = Convert.ToInt32(userChoicePicture) - 1;
                
                //full path of a file selected
                string filePath = pathOfPics + @"\" + fileNames[realUserChoice].ToString();

                //user input : picture caption
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What would you like to say with your picture on twitter?");
                Console.ResetColor();
                //capture input
                string textToTweet = Console.ReadLine();
                Console.WriteLine("\n");



                /**************************send picture tweet*******************************/
                //exception unhandled if no input
                byte[] file1 = File.ReadAllBytes(filePath);
                var media = Upload.UploadBinary(file1);
                Tweet.PublishTweet(textToTweet + " " + DateTime.Now, new PublishTweetOptionalParameters
                {
                    Medias = new List<IMedia> { media }
                });
            }   // tweet media

            else if (userInput == "3")
            {
                /**************************schedule a tweet*******************************/
                DateTime currentTime = DateTime.Now;
                Console.WriteLine("You will post a tweet at a later date");
                Console.WriteLine("How many days do you want to wait?");
                int userAddDays = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("How many hours do you want to wait?");
                int userAddHours = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("How many minutes do you want to wait?");
                int userAddMinutes = Convert.ToInt32(Console.ReadLine());

                /***********************add time to the current time************************/

                //get current time
                Console.WriteLine("Current time = {0}", currentTime);

                //add days, hours, minutes
                DateTime newTime = DateTime.Now
                    .AddDays(userAddDays)
                    .AddHours(userAddHours)
                    .AddMinutes(userAddMinutes);

                //print time of scheduled post
                Console.WriteLine("Your tweet will be published at " + newTime);

                /*****compare times******/

                //datetime compare
                int timeCompare = DateTime.Compare(newTime, currentTime);
                Console.WriteLine(timeCompare + " newTime, currentTime"); //1 if the added time is later than now
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What would you like to say on twitter?");
                Console.ResetColor();
                string textToTweet = Console.ReadLine();

                
                /*  
                 *  <0 − If date1 is earlier than date2
                    0 − If date1 is the same as date2
                    >0 − If date1 is later than date2
                */

                //waits until time == user added time
                while (timeCompare > 0) //addTime is later than currentTime
                {
                    //end while loop, should end when timeCompare is changed
                    Console.WriteLine("It is not time to post yet, the program will try again in 30 seconds...");
                    System.Threading.Thread.Sleep(30000);   //sleep for 60 seconds
                    timeCompare = DateTime.Compare(newTime, DateTime.Now);
                    Console.WriteLine($"{timeCompare} ");

                    if (timeCompare < 0)
                    {
                        break;
                    }
                }

                //runs program
                if (timeCompare < 0) //currentTime is later than addTime
                {
                        Tweet.PublishTweet(textToTweet + " " + DateTime.Now);
                        Console.WriteLine("Tweet Should have sent at " + DateTime.Now);
                }
                

                

                /*****post tweet******/

                
                


                while (newTime > currentTime)
                {
                    Console.WriteLine("It is not time to post yet, the program will try again in 60 seconds...");
                    System.Threading.Thread.Sleep(60000);   //sleep for 60 seconds
                    if (newTime <= currentTime)
                    {
                        break;
                    }
                }

                

                
                


            }   //tweet later

            else if (userInput == "4")
            {
                /**************************schedule a picture*******************************/
                Console.WriteLine("Schedule a tweet with a picture for later?");
            }   //tweet media later

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not an Option");

            }   //return error



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bot ended");
            Console.ResetColor();

            Console.ReadLine();
        }

        
    }
}
