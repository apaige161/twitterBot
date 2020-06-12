using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;

namespace talkToTInvi
{
    class Program
    {
        static void Main(string[] args)
        {
            /*****talk to twitterBot helper program******/



            // Set up your credentials (https://apps.twitter.com)
            Auth.SetUserCredentials(Sensitive.apikey, Sensitive.apikeySecret, Sensitive.accesstoken, Sensitive.accesstokenSecret); 

            //get current time
            DateTime currentTime = DateTime.Now;

            //get the user added time && text to send from main program

            //get textToTweet from main program
            string textToTweet = args[0]; //grabs the text of tweet
            Console.WriteLine(textToTweet + " Should print the tweet to go out");

            //get textToTweet from main program
            string newTimeDateString = args[1]; //only grabs the date
            Console.WriteLine(newTimeDateString + " Should print the date tweet is schedualed to go out in string format");

            string newTimeTimeString = args[2]; //grabs the time
            Console.WriteLine(newTimeTimeString + " Should print the time tweet is schedualed to go out in string format");

            string newTimeAmPmString = args[3]; //only grabs AM or PM
            Console.WriteLine(newTimeAmPmString + " Should print the AM or PM tweet is schedualed to go out in string format");

            Console.ReadLine();

            string newTimeString = newTimeDateString + " " + newTimeTimeString + " " + newTimeAmPmString;
            Console.WriteLine(newTimeString + " is a string of the new date time");



            //having issues parsing DateTime, currently capturing the time in {1} rather than the whole DateTime
            DateTime NewRealTime = DateTime.Parse(newTimeString);

            Console.WriteLine(NewRealTime + " Should print out full date time of type DateTime");
            Console.ReadLine();

            //pass in variables for the while loop and post tweet
            Worker(NewRealTime, currentTime, textToTweet);
        }

        public static void Worker(DateTime newTime, DateTime currentTime, string textToTweet)
        {
            //test
            Console.WriteLine(newTime);
            Console.WriteLine(currentTime);
            Console.WriteLine(textToTweet);
            Console.ReadLine();

            //datetime compare for while loop
            int timeCompare = DateTime.Compare(newTime, currentTime);



            /*  timeCompare:
             *  <0 − If date1 is earlier than date2
                0 − If date1 is the same as date2
                >0 − If date1 is later than date2
            */

            //waits until addTime is later than currentTime
            while (timeCompare > 0)
            {
                //end while loop, should end when timeCompare is changed
                Console.WriteLine("It is not time to post yet, the program will try again in 30 seconds...");
                Thread.Sleep(30000);   //sleep for 30 seconds
                timeCompare = DateTime.Compare(newTime, DateTime.Now);
                Console.WriteLine($"{timeCompare} ");

                if (timeCompare < 0)
                {
                    break;
                }
            }

            /*****post tweet******/
            //runs program after while loop completes
            if (timeCompare < 0) //currentTime is later than addTime
            {
                Tweet.PublishTweet(textToTweet);
                Console.WriteLine("Tweet Should have sent at " + DateTime.Now);
            }

            Console.ReadLine();
        }

    }
}
