using System.ComponentModel.DataAnnotations;
using System;

namespace Diondachi.Models
{
    public class Dachi
    {
        public static Random rand = new Random();

        [Display(Name = "Enter a name for your Diondachi:")]
        [MinLength(3)]
        public string Name { get; set; }
        public int Happiness { get; set; }

        public int Fullness { get; set; }

        public int Energy { get; set; }

        public int Meals { get; set; }

        // , int Happiness, int Fullness, int Energy, int Meals
        public Dachi()
        {
            Happiness = 20;
            Fullness = 20;
            Energy = 50;
            Meals = 3;
        }

        public void Play()
        {
            int maybe = rand.Next(1, 5);
            if (Energy >= 5)
            {

                if (maybe == 1)
                {
                    Energy -= 5;
                    Console.WriteLine("Your Diondatchi just isn't feeling it. No Happiness is gained.");
                }
                else
                {
                    Energy -= 5;
                    Happiness += rand.Next(5, 11);
                }
            }
            else
            {
                Console.WriteLine("Not enough energy!");
            }
        }

        public void Feed()
        {
            int maybe = rand.Next(1, 5);

            if (Meals > 0)
            {
                if (maybe == 1)
                {
                    Meals -= 1;
                    Console.WriteLine("Your Diondachi does not approve of this diet.");
                }
                else
                {
                    Meals -= 1;
                    Fullness += rand.Next(5, 11);
                }
            }
            else
            {
                Console.WriteLine("you cannot feed your Dojodachi if you do not have meals");
            }


        }
        public void Work()
        {
            if (Energy >= 5)
            {
                Energy -= 5;
                Meals += rand.Next(1, 4);
            }
            else
            {
                Console.WriteLine("Not enough energy!");
            }
        }
        public void Sleep()
        {
            Energy += 15;
            Fullness -= 5;
            Happiness -= 5;
        }
    }

}