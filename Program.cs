using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Haunted_Computer
{
    class Program
    {
        /// <summary>
        ///     point of insertion. takes duration from user and handles threads
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //defines varaible for duration and takes input from user
            int Duration;
            Console.WriteLine("Enter number of seconds for program to run");
            Duration = Convert.ToInt32(Console.ReadLine()) * 1000;

            //creates threads from functions
            Thread MouseThread = new Thread(new ThreadStart(MoveCursorRandomThread));
            Thread KeyboardThread = new Thread(new ThreadStart(RandomKeyPressThread));
            Thread PopupThread = new Thread(new ThreadStart(RandomPopupThread));

            //starts threads
            MouseThread.Start();
            KeyboardThread.Start();
            PopupThread.Start();

            //runs threads for specified duration
            Thread.Sleep(Duration);

            //aborts threads
            MouseThread.Abort();
            KeyboardThread.Abort();
            PopupThread.Abort();
            Console.WriteLine("Threads aborted. Press Enter to exit");
            Console.Read();
        }

        #region Threads

        /// <summary>
        ///     defines RanNum as new random
        /// </summary>
        public static Random RanNum = new Random();

        /// <summary>
        ///     function to move the cursor randomly
        /// </summary>
        public static void MoveCursorRandomThread()
        {
            while (true)
            {
                //changes x and y co-ordinate of cursor by a random number 1-25 either direction
                Cursor.Position = new Point(Cursor.Position.X + (-25 + RanNum.Next(50)), Cursor.Position.Y + (-25 + RanNum.Next(50)));
               
                //outputs to console to confirm (testing)
                Console.WriteLine("(X = " + Cursor.Position.X + ") (Y = " + Cursor.Position.Y + ")");
                
                //waits 100ms
                Thread.Sleep(50);
            }            
        }

        /// <summary>
        ///     function to cause random keyboard input
        /// </summary>
        public static void RandomKeyPressThread()
        {
            while (true)
            {
                //50% chance to enter a random letter
                if (RanNum.Next(100) > 25)
                {
                    // Generate a random capitol letter
                    char key = (char)(RanNum.Next(25) + 65);

                    // 50/50 make it lower case
                    if (RanNum.Next(2) == 0)
                    {
                        key = Char.ToLower(key);
                    }

                    //sends keypress
                    SendKeys.SendWait(key.ToString());
                }

                //wait half a second
                Thread.Sleep(RanNum.Next(200));
            }
        }

        /// <summary>
        ///     function to cause popups at random intervals
        /// </summary>
        public static void RandomPopupThread()
        {
            //integer to store selection of which popup to show
            int PopupChoice;
            while (true)
            {

                //50% chance to show a popup
                if (RanNum.Next(100) > 50)
                {

                    //chose 1 of 3 popups
                    PopupChoice = RanNum.Next(3);
                    
                    //case selection to chose a popup to show
                    switch (PopupChoice)
                    {
                        case 0:
                            {
                                MessageBox.Show("Error: You are running low on memory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        case 1:
                            {
                                MessageBox.Show("Error: Your CPU is about to EXPLODE!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        case 2:
                            {
                                MessageBox.Show("Error: asjvfbaishncwigsjregrfagbawvbargbawubgka4uygcmanbiyuhnmnwtkuqu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                    }
                }
            }
        }

        #endregion

    }
}
