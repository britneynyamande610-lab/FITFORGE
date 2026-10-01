using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitForge_PRG182
{
    internal class Program
    {
        //enum menu for options
        enum MenuOption
        {
            CaptureMemberData = 1,
            CheckRewardQual,
            ShowFitForgeStats,
            Exit
        }

        //2D arrays to store data & count variabes to keep track of number of applicants
        static string[,] applicants = new string[100, 8];
        static int applicantCount = 0;
        static string[,] qualifiedApplicants = new string[100, 8];
        static int qualifiedCount = 0;
        static string[,] notQualified = new string[100, 8];
        static int notQualifiedCount = 0;



        //Member capturing method
        static ArrayList CaptureMemberData()
        {
            ArrayList applicantList = new ArrayList();
            bool running = true;
            while (running == true)
            {


                Console.Clear();
                // checks if number of applicants exceed array size
                if (applicantCount >= applicants.GetLength(0))
                {
                    Console.WriteLine("Applicant List is Full");
                    return applicantList;
                }
                DateTime joinDate = DateTime.Now;
                //asking for details
                Console.WriteLine("Enter Applicant Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Applicant Membership Rank: ");
                string rank = Console.ReadLine();
                while (true)
                {
                    Console.WriteLine("Enter Applicant's Join Date (MM/dd/yyyy)");
                    string userInput = Console.ReadLine();

                    if (DateTime.TryParse(userInput, out joinDate))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please try again.");
                    }
                }

                Console.WriteLine("Enter Number of Smoothies Purchased: ");
                string smoothiesPurchased = Console.ReadLine();
                Console.WriteLine("Enter Personal Best Treadmill Distance in km: ");
                string treadmillDistance = Console.ReadLine();
                Console.WriteLine("Enter Current employment status (y/n)");
                string employmentStatus = Console.ReadLine().ToLower();
                Console.WriteLine("Enter Favourite Smoothie Flavour: ");
                string smoothieFlavour = Console.ReadLine();
                Console.WriteLine("Enter Number of Smoothies Consumed Since Joining:");
                string smoothiesConsumed = Console.ReadLine();


                //actually storing the data
                applicants[applicantCount, 0] = name;
                applicants[applicantCount, 1] = rank;
                applicants[applicantCount, 2] = joinDate.ToString("dd/MM/yyyy");
                applicants[applicantCount, 3] = smoothiesPurchased;
                applicants[applicantCount, 4] = treadmillDistance;
                applicants[applicantCount, 5] = employmentStatus;
                applicants[applicantCount, 6] = smoothieFlavour;
                applicants[applicantCount, 7] = smoothiesConsumed;
                applicantCount++;

                Console.WriteLine("Do you want to add another applicant? (y/n): ");
                string Response = Console.ReadLine().ToLower();

                running = (Response == "y") ? true : false;
            }
            return applicantList;

        }



        //Method to check and display all qualified members for wellness reward
        static void CheckRewardQual()
        {
            Console.Clear();
            //checks if there are even any applicants to assess
            if (applicantCount == 0)
            {
                Console.WriteLine("No Applicants to Check");
                return;
            }
            qualifiedCount = 0;
            notQualifiedCount = 0;

            //looping through the members to check who qualifies
            bool Qualified = false;

            for (int i = 0; i < applicantCount; i++)
            {
                // connecting? the data from array to use for eligibility check; the number need to be converted first from string to int/double
                string name = applicants[i, 0];
                int rank;
                int smoothiesConsumed;
                double treadmillDistance;
                string employmentStatus = applicants[i, 5];
                string faveflavour = applicants[i, 6];



                bool validSmoothies = int.TryParse(applicants[i, 7], out smoothiesConsumed);
                bool validDistance = double.TryParse(applicants[i, 4], out treadmillDistance);
                bool validRank = int.TryParse(applicants[i, 1], out rank);
                bool vaildjoinDate = DateTime.TryParse(applicants[i, 2], out DateTime joinDate);
                bool validTwoYearMembership = validDistance && (DateTime.Now - joinDate).TotalDays >= 730;

                if ((validSmoothies && smoothiesConsumed > 20) || faveflavour == "ChocoChurned Chaos")
                {
                    Console.WriteLine($"{name}: Does not qualify for the reward.");
                    notQualified[notQualifiedCount, 0] = name;
                    notQualifiedCount++;
                    continue;
                }


                if (
                    employmentStatus == "y" &&
                    validRank && validDistance && validSmoothies && validTwoYearMembership &&
                    (rank > 2000 || treadmillDistance > 20) &&
                    smoothiesConsumed > 4
                )
                {
                    Console.WriteLine($"{name}: Qualifies for the reward!");
                    qualifiedApplicants[qualifiedCount, 0] = name;
                    qualifiedCount++;
                }
                else
                {
                    Console.WriteLine($"{name}: Does not qualify for the reward.");
                    notQualified[notQualifiedCount, 0] = name;
                    notQualifiedCount++;
                }
            }

        }
        //counts who qualified and who did not
        static void QualCount()
        {
            int qualifiedCountFinal = qualifiedCount;
            int notQualifiedCountFinal = notQualifiedCount;
        }

        // method to display FitForge stats using count variables
        static void ShowFitForgeStats()
        {
            Console.Clear();
            Console.WriteLine($"The number of applicants who qualified: {qualifiedCount}");
            Console.WriteLine();
            Console.WriteLine($"The number of applicants who were denied: {notQualifiedCount}");
            Console.WriteLine();
            Console.WriteLine($"Total number of members: {applicantCount}");
            Console.WriteLine();


            if (applicantCount > 0)
            {
                double percentQualified = ((double)qualifiedCount / applicantCount) * 100;
                Console.WriteLine($"The percentage of people who qualified: {percentQualified}%");
            }
            else
            {
                Console.WriteLine("The percentage of people who qualified: N/A ");
            }
            Console.WriteLine();

        }

        // main method, with main loop and switch case
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("--------------FitForge Wellness Centre-------------");
                Console.WriteLine("***************************************************");
                Console.WriteLine("1. Capture Member Details");
                Console.WriteLine("2. Check Wellness Reward Qualification");
                Console.WriteLine("3. Show FitForge Stats");
                Console.WriteLine("4. Exit");
                Console.WriteLine("***************************************************");

                int choice;
                bool valid = int.TryParse(Console.ReadLine(), out choice);

                if (!valid || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid Input. Please Try Again");
                    continue;
                }

                MenuOption option = (MenuOption)choice;

                switch (option)
                {
                    case MenuOption.CaptureMemberData:
                        CaptureMemberData();
                        break;
                    case MenuOption.CheckRewardQual:
                        CheckRewardQual();
                        break;
                    case MenuOption.ShowFitForgeStats:
                        ShowFitForgeStats();
                        break;
                    case MenuOption.Exit:
                        running = false;
                        break;

                }
            }












            Console.ReadLine();
        }
    }
}

