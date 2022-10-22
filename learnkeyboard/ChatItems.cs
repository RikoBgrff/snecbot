using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnkeyboard
{
    public static class ChatItems
    {
        public static string AboutUs { get; set; } = "About Us";
        public static string Menu { get; set; } = "Menu";
        public static string Restaurants { get; set; } = "Restaurants";
        public static string TakeAway { get; set; } = "Take Away";
        public static string Delivery { get; set; } = "Delivery";
        public static string Contacts { get; set; } = "Contacts";
        public static string ContactWthManager { get; set; } = "Contact With Manager";
        public static string Soups { get; set; } = "Soups";
        public static string Drinks { get; set; } = "Drinks";
        public static string FastFood { get; set; } = "Fast Food";
        public static string Sauces { get; set; } = "Sauces";
        public static string Starters { get; set; } = "Starters";
        public static string AboutUsAnswer { get; set; } = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
            " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
            "when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
            " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
            " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages," +
            " and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        public static string BackToMenu { get; set; } = "🔙Back to Main Menu";
        public static string Contact { get; set; } = @"🕛 09:00 - 21:00
📞 +994 010 100 01 01
📧 snecaz.info@gmail.com
🌐 We are in social media:";
        public static string MenuMsg { get; set; } = @"Snac Bot offers you the best meal with the best price!
These are some of them,we will expand our categories with precious meals and drinks as soon as possible.";
        public static string Welcome { get; set; } = @"👋Welcome To Snac Bot! 
ℹ️I will help you to choose best meal for you.
✨Choose Menu to start ordering!";
        public static string Cart { get; set; } = "🛒 Cart";
        public static string TakeAwayDef { get; set; } = @"Pickup is possible from any of our restaurants,🕐
no earlier than half an hour after paying for the order.
📍 You choose a takeout restaurant when placing an order";
        public static string DeliveryMsg { get; set; } = @"🚚Delivery to Baku and Sumqayit:
‣ Delivery price: x AZN
‣ Minimum amount of shopping for free delivery:
x AZN
‣ Maximum Delivery Time: 40 min.
if delivery time is more than 40 min, you will be free of delivery charge.

💰 You can pay for your order through EasyPay directly in this chatbot during checkout";
    }
}