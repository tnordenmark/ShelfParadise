using System;
using System.Collections.Generic;
using System.Linq;

namespace ShelfParadise
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create instance of the ShopLogic class to handle all
            // operations
            ShopLogic shop = new ShopLogic();

            // Populate the shop with a bunch of items at runtime since
            // we do not store them persitently
            shop.AddItem(1, "Billy", "Bokhyllor", 399);
            shop.AddItem(2, "Fnask", "Madrass", 749);
            shop.AddItem(3, "Sommarbrus", "Mattor", 99);
            shop.AddItem(4, "Brunte", "Häst", 19900);
            shop.AddItem(5, "Greger", "Hund", 9750);

            // Declare the menu choice char outside of the scope of the
            // loop to be able to use it for while after the loop
            char mainMenuChoice;

            // Begin switch statement and loop for the various menus
            do
            {
                // Show main menu and get user input
                ShowMainMenu();
                Console.Write("Ange val: ");
                mainMenuChoice = GetMainMenuChoice();

                switch(mainMenuChoice)
                {
                    case '1':
                        // Declare new variable to not have choice leak
                        // 0 to the main menu loop
                        char productMenuChoice;

                        // Begin switch statement and loop for the Product
                        // sub menu
                        do
                        {
                            // Show product sub menu and get user input
                            ShowProductMenu();
                            Console.Write("Ange val: ");
                            productMenuChoice = GetProductMenuChoice();

                            switch(productMenuChoice)
                            {
                                case '1':
                                    // We could have done the sorting on the fly here
                                    // but putting the code in a method seems better
                                    // for reusability and debugging
                                    //List<Item> itemListPrice = shop.GetItemList().OrderBy(i => i.Pris).ToList();

                                    // Fetch product list sorted by price
                                    List<Item> itemListByPrice = shop.GetItemListByPrice();

                                    // Print product header and sorted product list
                                    PrintProductHeader();
                                    foreach(Item item in itemListByPrice)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case '2':
                                    // Fetch product list sorted by name
                                    List<Item> itemListByName = shop.GetItemListByName();

                                    // Print product header and sorted product list
                                    PrintProductHeader();
                                    foreach(Item item in itemListByName)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case '3':
                                    // Fetch list to be able to check if part number exists
                                    List<Item> inventoryList = shop.GetStoreInventory();
                                    // Declare string outside of the scope of the loop to
                                    // be able to use it as a condition for while
                                    string part;

                                    // Ask user to keep adding items to the shopping cart unless user
                                    // inputs 'q'
                                    do
                                    {
                                        Console.Write("Ange artikelnummer att lägga till i kundvagn ('q' för att avsluta): ");
                                        part = Console.ReadLine();
                                        int partId;
                                        int.TryParse(part, out partId);

                                        // Check if part number exists
                                        if(inventoryList.Exists(o => o.ArtikelNummer == partId))
                                        {
                                                shop.AddItemToCart(partId);
                                        }
                                        else
                                        {
                                            // Print error message unless input is 'q' to quit
                                            if(part[0] != 'q')
                                            {
                                                Console.WriteLine("Artikelnumret finns inte.");
                                            }
                                        }
                                    // First char of input string is not 'q'
                                    } while(part[0] != 'q'); 
                                    break;
                                case '0':
                                    break;
                                default:
                                    // Input error control is handled in the functions that
                                    // gets the actual input
                                    break;
                            }
                        } while(productMenuChoice != '0');
                        break;
                    case '2':
                        Console.Write("Ange namnet på produkten du söker: ");
                        string productName = Console.ReadLine();
                        // Search for product by name
                        Item itemMatch = shop.FindProductByName(productName);

                        // Check whether the returned itemMatch variable is not
                        // empty which means the specified item was found
                        if(itemMatch != null)
                        {
                            // Print product header and item if found
                            PrintProductHeader();
                            Console.WriteLine(itemMatch);
                        }
                        // If no match print error message
                        else
                        {
                            Console.WriteLine("Ingen produkt hittades.");
                        }
                        break;
                    case '3':
                        List<Item> cartList = shop.GetShoppingCartContents();

                        // Print product header and shopping cart contents
                        PrintProductHeader();
                        foreach(Item item in cartList)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case '4':
                        // Do checkout
                        List<Item> cartOutChck = shop.GetShoppingCartContents();

                        // Print product header and shopping cart contents
                        PrintProductHeader();
                        int sum = 0;
                        foreach(Item item in cartOutChck)
                        {
                            Console.WriteLine(item);
                            sum += item.Pris;
                        }
                        Console.WriteLine("Totalt att betala: {0} kr", sum);
                        Console.Write("Är du säker på att du vill betala (J/N): ");
                        string inputChck = Console.ReadLine();
                        if(inputChck == "J" || inputChck == "j")
                        {
                            shop.EmptyShoppingCart();
                        }
                        else if(inputChck == "N" || inputChck == "n")
                        {
                            Console.WriteLine("Avbryter utcheckningen.");
                        }
                        break;
                    case '0':
                        Console.WriteLine("Tryck på valfri tangent för att avsluta programmet.");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    default:
                        // Input error control is handled in the functions that
                        // gets the actual input
                        break;
                }
            } while(mainMenuChoice != '0');
        } // End of Main()

        // Show the Main Menu.
        static void ShowMainMenu()
        {
            Console.WriteLine("\n### Shelf Paradise ###");
            Console.WriteLine("1) Visa produkter");
            Console.WriteLine("2) Sök vara");
            Console.WriteLine("3) Visa kundvagn");
            Console.WriteLine("4) Gå till kassan");
            Console.WriteLine("0) Avsluta program");
        }

        // Show the product sub menu.
        static void ShowProductMenu()
        {
            Console.WriteLine("\n### Visa produkter ###");
            Console.WriteLine("1) Sortera efter pris");
            Console.WriteLine("2) Sortera efter namn");
            Console.WriteLine("3) Lägg vara i kundvagn");
            Console.WriteLine("0) Till huvudmeny");
        }

        // Handle user input for the Main Menu choice.
        static char GetMainMenuChoice()
        {
            string input = Console.ReadLine();
            char choice;

            if(!(char.TryParse(input, out choice)) && choice < '0' || choice > '4')
            {
                Console.WriteLine("Var god välj ett giltigt menyval.");
            }

            return choice;
        }

        // Handle user input for the Product Menu choice.
        static char GetProductMenuChoice()
        {
            string input = Console.ReadLine();
            char choice;

            if(!(char.TryParse(input, out choice)) && choice < '0' || choice > '3')
            {
                Console.WriteLine("Var god välj ett giltigt menyval.");
            }

            return choice;
        }

        // Print a header of product attributes.
        static void PrintProductHeader()
        {
            string artnr = "Artikelnummer";
            string namn = "Namn";
            string kategori = "Kategori";
            string pris = "Pris";

            // Align text to the left and in fields of 20 characters width
            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", artnr, namn, kategori, pris);
            Console.WriteLine("------------------------------------------------------------------");
        }
    }
}
