using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreItemPriceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, double> dicWeekdayDiscount = new Dictionary<string, double>();
            dicWeekdayDiscount.Add("Monday", 2);
            dicWeekdayDiscount.Add("Tuesday", 0);
            dicWeekdayDiscount.Add("Wednesday", 5);
            dicWeekdayDiscount.Add("Thursday", 0);
            dicWeekdayDiscount.Add("Friday", 0);
            dicWeekdayDiscount.Add("Saturday", 0);
            dicWeekdayDiscount.Add("Sunday", 0);
            double discount = 0;
            bool isDayDiscount = false;

            Dictionary<string, double> dicItemDiscount = new Dictionary<string, double>();
            dicItemDiscount.Add("Thumbs up", 10);
            dicItemDiscount.Add("Toilet Cleaner", 10);
            dicItemDiscount.Add("Mangoes", 0);
            dicItemDiscount.Add("Sugar", 0);
            dicItemDiscount.Add("Bulbs", 0);
            dicItemDiscount.Add("Tea", 5);
            dicItemDiscount.Add("Oil", 25);

            string currentDay = DateTime.Now.DayOfWeek.ToString();
            discount = dicWeekdayDiscount.GetValueOrDefault(currentDay);
            isDayDiscount = discount > 0 ? true : false;

            if (isDayDiscount)
                Console.WriteLine("Date: " + DateTime.Now.ToShortDateString() + " " + currentDay + "'s discount on total bill: " + discount + "%");
            else
            {
                int ctr = 1;
                Console.WriteLine("Date: " + DateTime.Now.ToShortDateString() + " Today's day of week: " + currentDay + ", get exciting discount on these items -");
                Dictionary<string, double> tempDict = dicItemDiscount.Where(p => p.Value > 0).ToDictionary(x => x.Key, x => x.Value);
                foreach (string val in tempDict.Keys)
                {
                    Console.WriteLine(ctr + ". " + val);
                    ctr++;
                }
            }

            FreshGrocery freshGrocery = new FreshGrocery();
            freshGrocery.Name = "Sugar";
            freshGrocery.Price = 40;
            freshGrocery.Weight = 0.5;
            freshGrocery.DayDiscount = isDayDiscount;
            freshGrocery.OrderedItem = "Weight:" + freshGrocery.Weight + " kg";
            freshGrocery.ItemDiscount = isDayDiscount ? 0 : dicItemDiscount.GetValueOrDefault(freshGrocery.Name);

            Grocery grocery = new Grocery();
            grocery.Name = "Thumbs up";
            grocery.Price = 50;
            grocery.Quantity = 2;
            grocery.DayDiscount = isDayDiscount;
            grocery.OrderedItem = "Quantity: " + grocery.Quantity;
            grocery.ItemDiscount = isDayDiscount ? 0 : dicItemDiscount.GetValueOrDefault(grocery.Name);

            ShoppingCart cart = new ShoppingCart();
            cart.Orders = new List<StoreItem>();
            cart.Orders.Add(freshGrocery);
            cart.Orders.Add(grocery);
            Console.WriteLine("Ordered items - ");
            int i = 1;
            foreach(var item in cart.Orders)
            {
                Console.WriteLine(i + ". " + item.Name + " Price: " + item.Price + " Ordered " + item.OrderedItem);
                i++;
            }
            
            double price = cart.Calculate();
            price -= price * discount / 100;
            Console.WriteLine("Price: {0}", price);
            Console.ReadLine();
        }
    }
}
