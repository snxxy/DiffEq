using System;
using System.Threading;
using System.Collections.Generic;

namespace Generator
{
    sealed class ConsoleMenu
    {
        private bool mainMenuFlag = true;
        private bool secondaryMenuFlag = true;

        private string eqInput = null;
        private string secondaryMenuInput = null;

        private Dictionary<int, int> userInput = new Dictionary<int, int>();
        private Generator generator = new Generator();

        public void InitMenu()
        {
            while (mainMenuFlag)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("Menu");
                Console.WriteLine("1) Generate equations");
                Console.WriteLine("2) Exit");
                string input = Console.ReadLine();
                {
                    switch (input)
                    {
                        case "1":
                            {
                                secondaryMenuFlag = true;
                                while (secondaryMenuFlag)
                                {
                                    Console.WriteLine("-----------------");
                                    Console.WriteLine("Choose mode:");
                                    Console.WriteLine("1) Database");
                                    Console.WriteLine("2) Standalone");
                                    Console.WriteLine("3) Back");
                                    secondaryMenuInput = Console.ReadLine();
                                    switch (secondaryMenuInput)
                                    {
                                        case "1":
                                            {
                                                GenerationMenus(useDb: true);
                                                secondaryMenuFlag = false;
                                                break;
                                            }
                                        case "2":
                                            {
                                                GenerationMenus(useDb: false);
                                                secondaryMenuFlag = false;
                                                break;
                                            }
                                        case "3":
                                            secondaryMenuFlag = false;
                                            Console.Clear();
                                            break;
                                        default:
                                            {
                                                Console.WriteLine("Wrong input, please try again");
                                                Console.WriteLine("Press any key to continue");
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                    }
                                }
                                break;
                            }
                        case "2":
                            {
                                mainMenuFlag = !mainMenuFlag;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong input, please try again");
                                Console.WriteLine("Press any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                    }
                }
                Thread.Sleep(5);
            }
        }

        private void GenerationMenus(bool useDb)
        {
            Console.Clear();
            bool genMenusFlag = true;
            while (genMenusFlag)
            {
                string input = null;
                Console.WriteLine("Enter equation type (1 or 2) or type 'gen' to start generating");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        {
                            if (userInput.ContainsKey(1))
                            {
                                Console.WriteLine("Separable variables");
                                Console.Write("Quantity: ");
                                eqInput = Console.ReadLine();
                                try
                                {
                                    Convert.ToInt32(eqInput);
                                }
                                catch
                                {
                                    Console.WriteLine("Wrong input, please try again");
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                if (Convert.ToInt32(eqInput) > 1000)
                                {
                                    Console.WriteLine("Wrong input, please try again");
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                userInput[1] = userInput[1] + Convert.ToInt32(eqInput);
                                Console.Clear();
                                Console.WriteLine("Added");
                            }
                            else
                            {
                                Console.WriteLine("Separable variables");
                                Console.Write("Quantity: ");
                                eqInput = Console.ReadLine();
                                try
                                {
                                    Convert.ToInt32(eqInput);
                                }
                                catch
                                {
                                    Console.WriteLine("Wrong input, please try again");
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                if (Convert.ToInt32(eqInput) > 1000)
                                {
                                    Console.WriteLine("Wrong input, please try again");
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                userInput.Add(1, Convert.ToInt32(eqInput));
                                Console.Clear();
                                Console.WriteLine("Added");
                            }
                            break;
                        }
                    case "2":
                        {
                            if (userInput.ContainsKey(2))
                            {
                                Console.WriteLine("Homogeneous");
                                Console.Write("Quantity: ");
                                eqInput = Console.ReadLine();                               
                                try
                                {
                                    Convert.ToInt32(eqInput);
                                }
                                catch
                                {
                                    Console.WriteLine("Wrong input, please try again");
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                if (Convert.ToInt32(eqInput) > 1000)
                                {
                                    Console.WriteLine("Wrong input, please try again");
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                userInput[2] = userInput[2] + Convert.ToInt32(eqInput);
                                Console.Clear();
                                Console.WriteLine("Added");
                            }
                            else
                            {
                                Console.WriteLine("Homogeneous");
                                Console.Write("Quantity: ");
                                eqInput = Console.ReadLine();
                                try
                                {
                                    Convert.ToInt32(eqInput);
                                }
                                catch
                                {
                                    Console.WriteLine("Wrong input, please try again");
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                if (Convert.ToInt32(eqInput) > 1000)
                                {
                                    Console.WriteLine("Wrong input, please try again");
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                userInput.Add(2, Convert.ToInt32(eqInput));
                                Console.Clear();
                                Console.WriteLine("Added");
                            }
                            break;
                        }
                    case "gen":
                        {
                            Console.Clear();
                            foreach (var eq in userInput)
                            {
                                Console.WriteLine("Generating {0} equations of type {1}", eq.Value, eq.Key);
                            }
                            Console.WriteLine("Please wait");
                            generator.ManageOrder(userInput, useDb);
                            Console.WriteLine("Press any key to continue");
                            userInput.Clear();
                            Console.ReadKey();
                            genMenusFlag = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input, please try again");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                }
            }           
        }
    }
}
