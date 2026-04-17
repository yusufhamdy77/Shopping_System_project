using System;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace Shopping_System_Project
{
    public class CartAction
    {
        public string Type { get; set; }
        public string ItemName { get; set; }
    }
    public class CartManager<T>
    {
        public List<string> Items = new List<string>();
        Stack<CartAction> Actions = new Stack<CartAction>();
        public void AddItem(string item)
        {
            Items.Add(item);

            CartAction action = new CartAction();
            action.Type = "Add";
            action.ItemName = item;

            Actions.Push(action);
        }

        // undo 
        public void Undo()
        {
            if (Actions.Count > 0)
            {
                CartAction lastAction = Actions.Pop();

                if (lastAction.Type == "Add")
                {
                    Items.Remove(lastAction.ItemName);
                    Console.WriteLine($"Undo: the Adding of -> {lastAction.ItemName}");
                }
                else if (lastAction.Type == "Remove")
                {
                    Items.Add(lastAction.ItemName);
                    Console.WriteLine($"Undo: the  Remove  -> {lastAction.ItemName}");
                }
            }
            else
            {
                Console.WriteLine("Nothing to undo");
            }
        }
        // removed by user

        public bool RemoveItem(string item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);

                CartAction action = new CartAction();
                action.Type = "Remove";
                action.ItemName = item;

                Actions.Push(action);

                return true;
            }
            else
            {
                return false;
            }
        }


    }








    internal class Program
    {

        static public void ShowOptions()
        {
            Console.WriteLine("press a number to Execute ");
            Console.WriteLine("1- to Add item to the cart ");
            Console.WriteLine("2- to View the Cart  ");
            Console.WriteLine("3- to Remove item from the cart ");
            Console.WriteLine("4- to Checkout ");
            Console.WriteLine("5- to Undo last action ");
            Console.WriteLine("6- to Exit ");

        }

        static void Main(string[] args)
        {
            CartManager<string> cart = new CartManager<string>();
            while (true)
            {
                ShowOptions();
                Console.WriteLine("-------Enter your choise------ ");
                string input = Console.ReadLine();
                int User_Choise = 0;
                if (int.TryParse(input, out User_Choise))
                {

                    switch (User_Choise)
                    {
                        case 1:

                            Console.WriteLine("Enter item name:");
                            string addItem = Console.ReadLine();

                            cart.AddItem(addItem);

                            Console.WriteLine("Item added successfully");

                            break;

                        case 2:
                            {
                                if (cart.Items.Count > 0)
                                {
                                    Console.WriteLine("Your Cart Items:");

                                    foreach (var cartItem in cart.Items)
                                    {
                                        Console.WriteLine("- " + cartItem);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Cart is empty");
                                }

                                break;
                            }

                        case 3:
                            {
                                Console.WriteLine("Enter item name to remove :");
                                string removeItem = Console.ReadLine();

                                bool result = cart.RemoveItem(removeItem);

                                if (result)
                                {
                                    Console.WriteLine("Item removed successfully");
                                }
                                else
                                {
                                    Console.WriteLine("Item not found");
                                }

                                break;
                            }



                        case 4:
                            {
                                if (cart.Items.Count > 0)
                                {
                                    Console.WriteLine("----- Checkout -----");
                                    Console.WriteLine("Your order:");

                                    foreach (var item in cart.Items)
                                    {
                                        Console.WriteLine("- " + item);
                                    }

                                    Console.WriteLine("Order confirmed ✔");

                                    cart.Items.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Cart is empty");
                                }

                                break;
                            }
                        case 5:
                            if (cart.Items.Count > 0)
                            {

                                cart.Undo();

                               
                            }
                            else
                            {
                                Console.WriteLine("Cart is empty");
                            }
                            break;


                        case 6:
                            return;
                          


                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a number.");
                }
            }
        }

        }

    }
