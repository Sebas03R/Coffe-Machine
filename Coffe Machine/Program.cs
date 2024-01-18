using System;
using System.Collections.Generic;

public class CoffeeMachine
{
    private Dictionary<string, double> resources = new Dictionary<string, double>
    {
        {"Water", 100},
        {"Milk", 100},
        {"Coffee", 100},
        {"Money", 0}
    };

    private Dictionary<string, double> drinkCosts = new Dictionary<string, double>
    {
        {"espresso", 1.5},
        {"latte", 2.5},
        {"cappuccino", 3.0}
    };

    public void Start()
    {
        while (true)
        {
            Console.Write("What would you like? (espresso/latte/cappuccino): ");
            string choice = Console.ReadLine();

            if (choice == "off")
            {
                break;
            }
            else if (choice == "report")
            {
                foreach (var resource in resources)
                {
                    Console.WriteLine($"{resource.Key}: {resource.Value}");
                }
            }
            else if (drinkCosts.ContainsKey(choice))
            {
                if (CheckResources(choice))
                {
                    ProcessCoins(choice);
                }
            }
        }
    }

    private bool CheckResources(string drink)
    {
        if (resources["Water"] < 50 || resources["Milk"] < 50 || resources["Coffee"] < 20)
        {
            Console.WriteLine("Sorry there is not enough resources.");
            return false;
        }

        return true;
    }

    private void ProcessCoins(string drink)
    {
        Console.Write("Please insert coins. ");
        double total = 0.0;
        total += PromptForCoin("quarters", 0.25);
        total += PromptForCoin("dimes", 0.10);
        total += PromptForCoin("nickels", 0.05);
        total += PromptForCoin("pennies", 0.01);

        if (total >= drinkCosts[drink])
        {
            double change = total - drinkCosts[drink];
            resources["Money"] += drinkCosts[drink];
            Console.WriteLine($"Here is your {drink}. Enjoy! Here is ${Math.Round(change, 2)} dollars in change.");
            resources["Water"] -= 50;
            resources["Milk"] -= 50;
            resources["Coffee"] -= 20;
        }
        else
        {
            Console.WriteLine("Sorry that's not enough money. Money refunded.");
        }
    }

    private double PromptForCoin(string coinName, double coinValue)
    {
        Console.Write($"How many {coinName}?: ");
        int count = int.Parse(Console.ReadLine());
        return count * coinValue;
    }
}

public class Program
{
    public static void Main()
    {
        CoffeeMachine coffeeMachine = new CoffeeMachine();
        coffeeMachine.Start();
    }
}
