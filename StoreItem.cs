using System;
using System.Collections.Generic;
using System.Text;

namespace StoreItemPriceCalculator
{
    abstract class StoreItem
    {
        private string name;
        private double price = 0;
        private bool dayDiscount;
        private double itemDiscount;
        private string orderedItem;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public bool DayDiscount
        {
            get { return dayDiscount; }
            set { dayDiscount = value; }
        }

        public double ItemDiscount
        {
            get { return itemDiscount; }
            set { itemDiscount = value; }
        }

        public string OrderedItem
        {
            get { return orderedItem; }
            set { orderedItem = value; }
        }
        public abstract double Calculate();
    }

    class FreshGrocery : StoreItem
    {
        private double weight = 0;

        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        public override double Calculate()
        {
            double calculatedPrice = this.Price * this.Weight;
            double discountedValue = calculatedPrice * ItemDiscount / 100;
            return calculatedPrice - discountedValue;
        }
    }

    class Grocery : StoreItem
    {
        private int quantity = 0;
        private double gst = 10; // In Percentage

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        public override double Calculate()
        {
            
            double calculatedPrice = (this.Price * this.Quantity);
            double discountedValue = calculatedPrice * ItemDiscount / 100;
            calculatedPrice = calculatedPrice - discountedValue;

            // GST
            if (calculatedPrice > 0)
            {
                calculatedPrice += calculatedPrice * (gst / 100);
            }

            return calculatedPrice;
        }
    }

    class ShoppingCart
    {
        private List<StoreItem> orders;

        public List<StoreItem> Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
            }
        }

        public double Calculate()
        {
            double price = 0;
            if (this.Orders != null)
            {

                foreach (StoreItem order in this.Orders)
                {
                    price += order.Calculate();
                }

            }
            return price;

        }
    }
}
