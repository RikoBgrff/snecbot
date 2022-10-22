using learnkeyboard;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
//payment token 
//5670237786:AAFqv_m8C9S_vXctVKQv0ahyIYO59LRO4uU
var botClient = new TelegramBotClient("5733001345:AAHzpVxFkjlnTPLWbxLshBKrI66wLzvb82A");
#region Variables

List<Category> Categories = new List<Category>()
{
    new Category
    {
        Id = 1,
        Name = "Drinks",
        ImageLink = "https://mindovermunch.com/wp-content/uploads/2021/10/Black-Light-Lemonade-5-300x300.jpg",
        SubCategories = new List<Category>()
        {
            new Category
            {
                Id=1,
                Name = "Hot Drinks",
                ImageLink = "https://latoucheksa.com/wp-content/uploads/2021/03/15-hot-drinks-300x300.jpg",
            },
            new Category
            {
                Id=2,
                Name = "Fizzy Drinks",
                ImageLink = "https://bayraktaronline.com/panel/resimler/kat1.jpg",
            },
            new Category
            {
                Id=3,
                Name = "Alcoholic Drinks",
                ImageLink = "https://seasonedskilletblog.com/wp-content/uploads/2020/12/0C7AE26D-8CC1-44C3-8183-66E80F2B6445-300x300.jpg",
            }
        }
    },
    new Category
    {
        Id=2,
        Name = "Fast Food",
        ImageLink = "https://images.deliveryhero.io/image/fd-tr/LH/s55s-hero.jpg?width=300&height=300&quality=45",
        SubCategories = new List<Category>()
        {
            new Category
            {
                Id=4,
                Name ="Burgers",
                ImageLink ="https://cevatusta.com.tr/wp-content/uploads/2020/12/kebap-burger-300x300.jpg",
            },
            new Category{
            Id=5,
            Name="Pizzas",
            ImageLink = "https://images.deliveryhero.io/image/fd-tr/LH/tho3-hero.jpg?width=300&height=300&quality=45"
            },
            new Category
            {
                Id=6,
                Name="Sandwiches",
                ImageLink = "https://www.homemadeinterest.com/wp-content/uploads/2016/04/Grilled-California-Club_featured-300x300-1.jpg",
            }
        }
    }
};


List<Product> products = new List<Product>()
{
    new Product
    {
       Id = 1,
        Name= "☕Fruit Tea",
        CategoryId = 1,
        Price = 3.00,
        Image="https://images.squarespace-cdn.com/content/v1/55bfb182e4b0d40fc717da51/1657828410433-JZZJK6GJMM0TV3XD48PL/R+-+Strawberry+Mango+%28cup%29+5534.jpg?format=300w"

    },
    new Product
    {
        Id = 2,
        Name= "☕Fresh Mint Tea",
        Price = 4.00,
        CategoryId = 1,
        Image="https://crushmag-online.com/wp-content/uploads/2017/09/Fresh-Mint-Tea-3x3-.jpg"
    },
    new Product
    {
        Id= 3,
        Name= "☕Hot Chocolate",
        Price = 2.40,
        CategoryId = 1,
        Image="https://celebratingsweets.com/wp-content/uploads/2015/12/Spiked-Hot-Chocolate-1-2-300x300.jpg"
    },
    new Product
    {
        Id=4,
        Name= "☕Chai",
        Price = 2.60,
        CategoryId = 1,
        Image="https://www.teaforturmeric.com/wp-content/uploads/2021/11/Masala-Chai-Tea-Recipe-Card-300x300.jpg"
    },
    new Product {
        Id=5,
        Name= "🍵Fresh Ginger Tea",
        CategoryId = 1,
        Image="https://www.cubesnjuliennes.com/wp-content/uploads/2021/12/Lemon-Ginger-Turmeric-Tea-Recipe-300x300.jpg",
    Price = 3.20
    },
     new Product
    {
       Id = 6,
        Name= "🥤Coca Cola 1l",
        CategoryId = 2,
        Price = 2.00,
        Image="https://i.etsystatic.com/32392079/r/il/97c159/3412665272/il_300x300.3412665272_79nb.jpg"
    },
    new Product
    {
        Id = 7,
        Name= "🥤Pepsi 1l",
        CategoryId = 2,
        Price = 2.00,
        Image="https://cdn.shopify.com/s/files/1/0529/4971/3065/products/new_pepsi_coca_cola_coke_soda_beer_bar_pub_neon_light_sign_17_x_14_ca4a3a96_637428_48fd3450-f99d-4ed1-a7ca-a569976d06f9_300x300.jpg?v=1610098269"
    },
    new Product
    {
        Id= 8,
        Name= "🥤Mountain Dew 0.33l",
        CategoryId = 2,
        Price = 1.80,
        Image="https://i.pinimg.com/originals/5a/f6/c1/5af6c143a52ddc5c2af5a18f56dea8f5.jpg"
    },
    new Product
    {
        Id=9,
        Name= "🥤Fanta 1l",
        CategoryId = 2,
        Price = 2.00,
        Image="https://cdn.shopify.com/s/files/1/0529/4971/3065/products/new_pepsi_coca_cola_coke_soda_beer_bar_pub_neon_light_sign_17_x_14_ca4a3a96_637428_48fd3450-f99d-4ed1-a7ca-a569976d06f9_300x300.jpg?v=1610098269"
    },
      new Product
    {
       Id = 10,
        Name= "🍷Aperol",
       CategoryId = 3,
        Price = 20.00,
        Image="https://www.aperolspritzsocials.com/wp-content/uploads/2020/06/Aperol-3-300x300.jpg"
    },
    new Product
    {
        Id = 11,
        Name= "🍷Jägermeister",
       CategoryId = 3,
        Price = 22.00,
        Image="https://images.ctfassets.net/mj09inqdun29/2HbB61j89GCMameACo0K48/aeaf8d719ac522ff669a54e369d39c66/music-events-share.jpg?w=300&h=300"
    },
    new Product
    {
        Id= 12,
        Name= "🍷Gordon's Gin",
       CategoryId = 3,
        Price = 18.00,
        Image="https://ginfling.dk/pub/media/catalog/product/cache/28173f7ef52d73b10590a3bcded6468d/g/o/gordons-pink-gin-1l_yhph7gogu3w2d82y.jpg"
    },
    new Product
    {
        Id=13,
        Name= "🍷Nar Çaxırı",
       CategoryId = 3,
        Price = 10.00,
        Image="https://bestdrinks.az/cdn/storage/product_images/FCGhrd3yhKejT4Due/HD/FCGhrd3yhKejT4Due.jpg"
    },
    new Product
    {
        Id = 14,
        Name = "Hamburger",
        CategoryId=4,
        Image="https://cevatusta.com.tr/wp-content/uploads/2020/12/kebap-burger-300x300.jpg",
        Price = 1.70
    },
    new Product
    {
        Id = 15,
        Name = "CheeseBurger",
        CategoryId=4,
        Image="https://cevatusta.com.tr/wp-content/uploads/2020/12/cheese-burger-300x300.jpg",
        Price = 1.90
    },
    new Product
    {
        Id = 16,
        Name="Big Mac Burger",
        CategoryId=4,
        Image ="https://images.deliveryhero.io/image/fd-tr/LH/xf2y-hero.jpg?width=300&height=300&quality=45",
        Price= 4.5,
    },
   new Product
   {
       Id=17,
       Name="Margarita Pizza",
       CategoryId=5,
       Image="https://www.pizzamonza.com.tr/wp-content/uploads/2016/08/margarita.jpeg",
       Price = 10.00
   },
   new Product
   {
       Id=18,
       Name="Sausage Pizza",
       CategoryId=5,
       Image="https://www.cupofzest.com/wp-content/uploads/2022/06/Chicken-Sausage-Pizza-with-Onions-and-Peppers-Thumbnail-300x300.jpg",
       Price=10.00,
   },
   new Product{
   Id=19,
   Name="Mushroom Pizza",
   CategoryId=5,
   Image="https://i.pinimg.com/736x/b5/3c/ee/b53cee0ecfcd3499cf1dbdf4bb80e069--pizza-lasagna-pasta-pizza.jpg",
   Price=10.00
   },
   new Product
   {
       Id=20,
       Name="Chicken Sandwich",
       CategoryId=6,
       Image="https://simply-delicious-food.com/wp-content/uploads/2020/07/Grilled-curried-chicken-sandwiches-4-300x300.jpg",
       Price=3.0
   },
   new Product
   {
       Id=21,
       Name="Egg Sandwich",
       CategoryId=6,
       Image="https://www.dadwithapan.com/wp-content/uploads/2016/05/Breakfast-Grilled-Cheese-Sandwich-13-300x300.jpg",
       Price=2.0
   },
   new Product{
   Id=22,
   Name ="Nutella Sandwich",
   CategoryId=6,
   Image="https://sp-ao.shortpixel.ai/client/to_webp,q_glossy,ret_img,w_200,h_200/https://paulasapron.com/wp-content/uploads/2021/12/5-Minute-Grilled-Nutella-and-Strawberry-Sandwich-1-1-300x300.jpg",
   Price=4.0
   }

};
Customer customer = new Customer();
customer.Name = String.Empty;
List<Product> basket = new List<Product>();
int globalId = 0;
#endregion Variables

using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }
};

botClient.StartReceiving(
    HandleUpdatesAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"I am listening @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type == UpdateType.Message && update?.Message?.Text != null)
    {
        await HandleMessage(botClient, update.Message);
        return;
    }

    if (update.Type == UpdateType.CallbackQuery)
    {
        await HandleCallbackQuery(botClient, update.CallbackQuery);
        return;
    }

}

async Task HandleMessage(ITelegramBotClient botClient, Message message)
{
    #region MainPanel
    if (message.Text == "/start" || message.Text == "/Start")
    {
        ReplyKeyboardMarkup keyboard_start = new(new[]
        {
            new KeyboardButton[] { "ℹ️ " + $"{ChatItems.AboutUs}", "📖 "+ $"{ChatItems.Menu}" },
            new KeyboardButton[] { "🍔 " + $"{ChatItems.Restaurants }", "📱"+$"{ChatItems.Contacts}" },
            new KeyboardButton[] { "🚶 " + $"{ChatItems.TakeAway}", "🚚"+$"{ChatItems.Delivery}"},
            new KeyboardButton[] {"🧑🏻‍💼" + $"{ChatItems.ContactWthManager}"},
        })
        {
            ResizeKeyboard = true
        };
        await botClient.SendPhotoAsync(message.Chat.Id, "https://i.ibb.co/9sYGzRs/snec.png", caption: $"{ChatItems.Welcome}", replyMarkup: keyboard_start);
    }
    if (message.Text.Contains(ChatItems.AboutUs))
    {

        ReplyKeyboardMarkup keyboard_AboutUs = new(new[]
       {
            new KeyboardButton[] {$"{ChatItems.BackToMenu}"},
        })
        {
            ResizeKeyboard = true
        };
        await botClient.SendTextMessageAsync(message.Chat.Id, @$" ℹ️ {ChatItems.AboutUsAnswer}", replyMarkup: keyboard_AboutUs);
    }

    if (message.Text.Contains(ChatItems.Menu) || message.Text == "/Menu" || message.Text == "/menu")
    {
        ReplyKeyboardMarkup keyboard_Menu = new(new[]
        {
            new KeyboardButton[] {$"🍹{ChatItems.Drinks}",$"🍔{ChatItems.FastFood}"},
            new KeyboardButton[] {$"{ChatItems.Cart}",$"{ChatItems.BackToMenu}"},
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(message.Chat.Id, $"Selected:{message.Text}", replyMarkup: keyboard_Menu);

    }
    foreach (var item in Categories)
    {
        InlineKeyboardButton button = new InlineKeyboardButton("str");
        InlineKeyboardMarkup keyboard_Drinks = new InlineKeyboardMarkup(button);
        InlineKeyboardMarkup categoryKeyboard;
        if (message.Text.Contains(item.Name))
        {
            foreach (var item2 in item.SubCategories)
            {
                {
                    categoryKeyboard = new(new[]
                           {
            new[]
            {
                InlineKeyboardButton.WithCallbackData($"{item2.Name}", $"{item2.Name}"),
            },
        });
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"🚀Sub Category:{item2.Name}", replyMarkup: categoryKeyboard);
                }
            }
            ReplyKeyboardMarkup keyboard_Product = new(new[]
       {
            new KeyboardButton[] {$"{ChatItems.Cart}",$"{ChatItems.BackToMenu}"},
        })
            { ResizeKeyboard = true };
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Choose:", replyMarkup: keyboard_Product);
        }
    }


    if (message.Text.Contains(ChatItems.Restaurants))
    {
        ReplyKeyboardMarkup keyboard_Restaurant = new(new[]
        {
            new KeyboardButton[] { "📍KFC Bulbul Pr.", $"📍Mc Donalds Ganjlik" },
            new KeyboardButton[] { "📍Kolorit", $"{ChatItems.BackToMenu}"},
        });
        await botClient.SendTextMessageAsync(message.Chat.Id, $"You Selected:{message.Text}", parseMode: ParseMode.Html, replyMarkup: keyboard_Restaurant);

    }
    if (message.Text.Contains(ChatItems.Contacts))
    {
        ReplyKeyboardMarkup keyboard_Contacts = new(new[]
     {
            new KeyboardButton[] {$"🔙{ChatItems.BackToMenu}"}
        })
        {
            ResizeKeyboard = true
        };
        InlineKeyboardMarkup keyboard_SocialMedia = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithUrl("Instagram", "https://www.instagram.com/snec_az/"),
                InlineKeyboardButton.WithUrl("Facebook", "https://www.facebook.com/"),
                InlineKeyboardButton.WithUrl("TikTok", "https://www.tiktok.com/"),
                InlineKeyboardButton.WithUrl("Whatsapp","https://wa.me/708397309"),
                InlineKeyboardButton.WithUrl("Site","https://www.snec.az")
            },
        });
        await botClient.SendTextMessageAsync(message.Chat.Id, $"{ChatItems.Contact}", replyMarkup: keyboard_SocialMedia);

    }
    if (message.Text.Contains(ChatItems.TakeAway))
    {
        ReplyKeyboardMarkup keyboard_TakeAway = new(new[]
        {
            new KeyboardButton[] { "📍Take Away Points", $"{ChatItems.BackToMenu}"},
        });
        await botClient.SendTextMessageAsync(message.Chat.Id, $"{ChatItems.TakeAwayDef}", parseMode: ParseMode.Html, replyMarkup: keyboard_TakeAway);
    }
    if (message.Text.Contains(ChatItems.Delivery))
    {
        await botClient.SendTextMessageAsync(message.Chat.Id, $"{ChatItems.DeliveryMsg}");
    }
    if (message.Text.Contains(ChatItems.ContactWthManager))
    {
        await botClient.SendContactAsync(message.Chat.Id, "+994708397309", firstName: "Arifali", lastName: "Baghirli", protectContent: true);
    }

    if (message.Text.Contains(ChatItems.BackToMenu))
    {
        ReplyKeyboardMarkup keyboard_BackToMenu = new(new[]
      {
            new KeyboardButton[] { "ℹ️ " + $"{ChatItems.AboutUs}", "📖 "+ $"{ChatItems.Menu}" },
            new KeyboardButton[] { "🍔 " + $"{ChatItems.Restaurants }", "📱"+$"{ChatItems.Contacts}" },
            new KeyboardButton[] { "🚶 " + $"{ChatItems.TakeAway}", "🚚"+$"{ChatItems.Delivery}"},
            new KeyboardButton[] {"🧑🏻‍💼" + $"{ChatItems.ContactWthManager}" },
        })
        {
            ResizeKeyboard = true
        };
        await botClient.SendTextMessageAsync(message.Chat.Id, "Choose:", replyMarkup: keyboard_BackToMenu);

    }

    if (message.Text.Contains(ChatItems.Cart) || message.Text == "/cart" || message.Text == "/CART")
    {
        if (basket.Count == 0)
        {
            ReplyKeyboardMarkup emptyCart = new(new[]
      {
                new KeyboardButton[] { $"{ChatItems.Menu}", $"{ChatItems.BackToMenu}" },
            }
            )
            {
                ResizeKeyboard = true
            };
            await botClient.SendTextMessageAsync(message.Chat.Id, $@"Your Cart is empty", ParseMode.Html, replyMarkup: emptyCart);
        }
        if (basket.Count > 0)
        {
            double total;
            ReplyKeyboardMarkup cartKeyboard = new(new[] { new KeyboardButton[] { "Confirm Cart", $"{ChatItems.BackToMenu}" } }) { ResizeKeyboard = true };
            await botClient.SendTextMessageAsync(message.Chat.Id, $@"Your Cart:", replyMarkup: cartKeyboard);
            foreach (var item in basket)
            {
                total = item.Count * item.Price;
                InlineKeyboardMarkup editCart = new(new[] {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("+", $"+{item.Id}"),
                InlineKeyboardButton.WithCallbackData("-",$"-{item.Id}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("❌",$"❌{item.Id}"),
            }
        });
                await botClient.SendPhotoAsync(message.Chat.Id, photo: item.Image, caption: @$"Name:{item.Name}
Count:{item.Count}
Total Fee:{total} AZN", replyMarkup: editCart);
            }

        }
    }
    if (message.Text == "Confirm Cart")
    {
        await botClient.SendTextMessageAsync(message.Chat.Id, "Please Confirm Your Information");
        InlineKeyboardMarkup fillData1_Keyboard = new(new[] {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Yes","manualFillUsername"),
                InlineKeyboardButton.WithCallbackData("No","autoFillUsername"),
            }
        });
        await botClient.SendTextMessageAsync(message.Chat.Id, @$"Do you want to change your username?
Your current username is your Telegram username:{message.From.Username}", replyMarkup: fillData1_Keyboard);
        if (customer.Name != String.Empty && customer.Number != String.Empty && customer.Address.Location != String.Empty)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, $@"Your Information:
Name:{customer.Name}
Location:{customer.Address.Location}
Number:{customer.Number}");
        }
    }

    if (message.Text.Contains("Take Away Points"))
    {
        ReplyKeyboardMarkup keyboard_rest = new(new[]
        {
            new KeyboardButton[] { "📍KFC Bulbul Pr.", "📍Mc Donalds Ganjlik"},
            new KeyboardButton[] { "📍Kolorit", $"{ChatItems.BackToMenu}"},
        })
        { ResizeKeyboard = true }
        ;
        await botClient.SendTextMessageAsync(message.Chat.Id, $"Selected:{message.Text}", replyMarkup: keyboard_rest);

    }
    #endregion MainPanel
    #region SharedRestaurants
    if (message.Text.Contains("KFC Bulbul Pr."))
    {
        InlineKeyboardMarkup keyboard_loc_rest_1 = new(new[]
     {
            new[]
            {
                InlineKeyboardButton.WithUrl("Google Maps","https://www.google.com/maps/dir//mc+donalds+genclik/@40.4065987,49.8406347,15z/data=!4m8!4m7!1m0!1m5!1m1!1s0x40307d5d4f50e1bd:0xaba6c59c865aa776!2m2!1d49.8536245!2d40.4001517")
            },
         });
        await botClient.SendPhotoAsync(message.Chat.Id, "https://kfcturkiye.com/uploads/images/en-yakin-kfc/restaurants-header-mobile.jpg", caption: "KFC Bulbul Pr.", parseMode: ParseMode.Html);

        await botClient.SendLocationAsync(message.Chat.Id, 40.4109823, 49.8406584, replyMarkup: keyboard_loc_rest_1);

    }
    if (message.Text.Contains("Mc Donalds Ganjlik"))
    {
        InlineKeyboardMarkup keyboard_loc_rest_2 = new(new[]
      {
            new[]
            {
                InlineKeyboardButton.WithUrl("Google Maps","https://www.google.com/maps/dir//mc+donalds+genclik/@40.4065987,49.8406347,15z/data=!4m8!4m7!1m0!1m5!1m1!1s0x40307d5d4f50e1bd:0xaba6c59c865aa776!2m2!1d49.8536245!2d40.4001517")
            },
         });
        await botClient.SendPhotoAsync(message.Chat.Id, "https://mcdonalds.az/images/a03562ae6f6f097c2a959b339ff4e405.jpg", caption: "Mc Donalds Ganjlik", parseMode: ParseMode.Html);
        await botClient.SendLocationAsync(message.Chat.Id, 40.3996194, 49.849897, replyMarkup: keyboard_loc_rest_2);

    }
    if (message.Text.Contains("Kolorit"))
    {
        InlineKeyboardMarkup keyboard_loc_rest_3 = new(new[]
     {
            new[]
            {
                InlineKeyboardButton.WithUrl("Google Maps","https://www.google.com/maps/dir//kolorit/@40.3733496,49.8061857,13z/data=!4m8!4m7!1m0!1m5!1m1!1s0x40307dafdfcc6afb:0x27215e3f82607687!2m2!1d49.8412051!2d40.3732895?hl=tr")
            },
         });

        await botClient.SendPhotoAsync(message.Chat.Id, "https://media-cdn.tripadvisor.com/media/photo-s/12/b1/9d/93/the-comfortable-and-distinctiv.jpg", caption: "Kolorit", parseMode: ParseMode.Html, replyMarkup: keyboard_loc_rest_3);
        await botClient.SendLocationAsync(message.Chat.Id, 40.3793865878, 49.8474747545);
    }
    #endregion SharedRestaurants
    #region UserValidation
    if (message.Text.Contains("/updatename"))
    {
        var name = message.Text.Remove(0, 12);
        Console.WriteLine(name);
        await botClient.SendTextMessageAsync(message.Chat.Id, $"Your name saved as:{name}");
        customer.Name = name;
        await botClient.SendTextMessageAsync(message.Chat.Id, @"Enter your home address,please type /updatelocation
(for example: /updatelocation Ibrahim Pasha Dadashov 66,104)");
    }
    if (message.Text.Contains("/updatelocation"))
    {
        var location = message.Text.Remove(0, 15);
        customer.Address.Location = location;
        await botClient.SendTextMessageAsync(message.Chat.Id, $"Your home address saved as:{location}");
        await botClient.SendTextMessageAsync(message.Chat.Id, @"Enter your phone number,please type /updatenumber
(for example: /updatenumber 0123456789, 012-345-6789, and (012) -345-6789)");
    }
    if (message.Text.Contains("/updatenumber"))
    {
        var number = message.Text.Remove(0, 14);
        Console.WriteLine(number);
        if (PhoneNumber.IsPhoneNbr(number))
        {
            customer.Number = number;
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Your number saved as:{number}");
            var btn = InlineKeyboardButton.WithCallbackData("Confirm Data", "cartConfirmed");
            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(btn);
            await botClient.SendTextMessageAsync(message.Chat.Id, @"Do you confirm your data?
You can change your data by using commands", replyMarkup: inlineKeyboard);
        }
        else
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, @"Your Phone number could not saved.
Please try again.
Use suggested format to confirm your number");
        }
    }
    if (message.Type == MessageType.Location)
    {

        //Console.WriteLine(message.Location.Longitude);
    }
    //    if (message.Text.Contains("/updatestreet"))
    //    {
    //        await botClient.SendTextMessageAsync(message.Chat.Id, @"Enter your street,please type /updatestreet
    //(for example: /updatestreet Jafar Jabbarli 39)");
    //        var street = message.Text.Remove(0, 13);
    //        customer.Address.Street = street;
    //        await botClient.SendTextMessageAsync(message.Chat.Id, $"Your name saved as:{street}");
    //    }
    //    if (message.Text.Contains("/updatebuilding"))
    //    {
    //        await botClient.SendTextMessageAsync(message.Chat.Id, @"Enter your building,please type /updatebuilding
    //(for example: /updatebuilding 39");
    //        var building = message.Text.Remove(0, 13);
    //        await botClient.SendTextMessageAsync(message.Chat.Id, $"Your name building saved as:{building}");
    //    }
    //    if (message.Text.Contains("/updateapart"))
    //    {
    //        await botClient.SendTextMessageAsync(message.Chat.Id, @"Enter your apartment No,please type /updateapart
    //(for example: /updatapart 117");
    //        var apartmentNo = message.Text.Remove(0, 13);
    //        await botClient.SendTextMessageAsync(message.Chat.Id, $"Your name apartment no saved as:{apartmentNo}");
    //    }

    //add your street
    //add your building
    //add your apartment number

}
#endregion UserValidation

async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    string captionTest = "";
    foreach (var category in Categories)
    {
        foreach (var subItem in category.SubCategories)
        {
            if (callbackQuery.Data.StartsWith($"{subItem.Name}"))
            {
                foreach (var prod in products)
                {
                    if (prod.CategoryId == subItem.Id)
                    {
                        captionTest = @$"{prod.Name}
💳{prod.Price} AZN/1 Piece
Lorem Ipsum is simply dummy text of the printing and typesetting industry.
Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
Lorem Ipsum is simply dummy text of the printing and typesetting industry.
Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
                        InlineKeyboardMarkup productKeyboard = new(new[]
                       {
                       new[]
                       {
                        InlineKeyboardButton.WithCallbackData("Add To Cart", $"addToBasket{prod.Id}"),
                        InlineKeyboardButton.WithCallbackData("Back To Menu", "backToMenu"),
                        },
        });
                        await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: prod.Image, caption: captionTest, replyMarkup: productKeyboard);

                        captionTest = String.Empty;
                    }
                }
                return;

            }
        }
    }
    if (callbackQuery.Data.Contains("❌"))
    {
        Regex re1 = new Regex(@"\d+");
        Match match = re1.Match(callbackQuery.Data);
        int id = int.Parse(match.Value);
        if (id == 0)
        {
        }
        else if (id > 0)
        {
            var test = basket.FirstOrDefault(x => x.Id == id);
            if (test != null)
            {
                var removeObj = basket.FirstOrDefault(x => x.Id == id);
                basket.Remove(removeObj);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{removeObj.Name} removed from your cart");
            }
        }
    }
    if (callbackQuery.Data.Contains("+"))
    {
        Regex re1 = new Regex(@"\d+");
        Match match = re1.Match(callbackQuery.Data);
        int id = int.Parse(match.Value);
        if (id == 0)
        {
        }
        else if (id > 0)
        {
            //id = id - 1;
            var test = basket.FirstOrDefault(x => x.Id == id);
            if (test != null)
            {
                test.Count++;
                Console.WriteLine($"Test Count:{test.Count}");
                Console.WriteLine($"In basket count:{basket.Find(x => x.Id == id).Count}");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @$"{basket.Find(x => x.Id == id).Name} Count Updated
Current Count:{basket.Find(x => x.Id == id).Count}");
                if (test.Count == 10)
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "You can order maximum 10 pieces of one product");
                }
            }
            if (test == null)
            {

            }
        }
    }
    if (callbackQuery.Data.Contains("-"))
    {
        Regex re1 = new Regex(@"\d+");
        Match match = re1.Match(callbackQuery.Data);
        int id = int.Parse(match.Value);
        if (id == 0)
        {

        }
        else if (id > 0)
        {
            //id = id - 1;
            var test = basket.FirstOrDefault(x => x.Id == id);
            if (test != null)
            {
                if (test.Count > 0)
                {
                    test.Count--;
                    Console.WriteLine($"Test Count:{test.Count}");
                    Console.WriteLine($"In basket count:{basket.Find(x => x.Id == id).Count}");
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @$"{basket.Find(x => x.Id == id).Name} Count Updated
Current Count:{basket.Find(x => x.Id == id).Count}");
                }
                if (test.Count <= 1)
                {
                    var removeObj = basket.FirstOrDefault(x => x.Id == id);
                    basket.Remove(removeObj);
                    Console.WriteLine(basket.Count);
                }
            }
            if (test == null)
            {

            }
        }
    }
    Product product1 = new Product();
    if (callbackQuery.Data.StartsWith("addToBasket"))
    {
        {
            Regex re = new Regex(@"\d+");
            Match m = re.Match(callbackQuery.Data);

            int id = int.Parse(m.Value);
            if (id == 0)
            {
            }
            else if (id > 0 && id <= products.Count)
            {
                id = id - 1;
                product1 = products.ElementAt(id);
                product1.Count++;
                InlineKeyboardMarkup editItemCount = new(new[] {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("+", $"+{product1.Id}"),
                InlineKeyboardButton.WithCallbackData("-",$"-{product1.Id}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("❌",$"❌{product1.Id}"),
            }
                });
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, product1.Image);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Edit Product Count:", replyMarkup: editItemCount);
                basket.Add(product1);

                Console.WriteLine($"Product with id:{m.Value} added to cart");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{product1.Name} added to cart");
            }


        }
    }

    if (callbackQuery.Data == "autoFillUsername")
    {
        customer.Name = callbackQuery.Message.From.Username;
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Your name saved as:{customer.Name}");
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Enter your home address,please type /updatelocation
(for example: /updatelocation Ibrahim Pasha Dadashov 66,104)");
    }
    if (callbackQuery.Data == "manualFillUsername")
    {

        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, @"Enter your name,please type /updatename
(for example: /updatename Arifali Baghirli");
    }
    if (callbackQuery.Data == "cartConfirmed")
    {
        if (customer.Name != String.Empty && customer.Number != String.Empty && customer.Address.Location != String.Empty)
        {
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $@"Your Information:
Name:{customer.Name}
Location:{customer.Address.Location}
Number:{customer.Number}");
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "There goes payment section");
        }
    }
}


Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Error:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
        _ => exception.ToString()
    };
    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
//CHEQUE
//    foreach (var item in basket)
//    {
//        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $@"
//{item.Name} - {item.Count}
//Customer Name:Name
//Delivery Address:
//Contact Number:
//Delivery Fee:5 AZN
//Order Fee:{item.Price * item.Count}
//Total Fee:{item.Price * item.Count + 5}
//Orders will be delivered in 30 min.");
//    }

//CHEQUE 2  
//await botClient.SendInvoiceAsync((message.Chat.Id), "Title", "Description", "Payload", cts, "AZN", suggestedTipAmounts: 5, needName: true, needShippingAddress: true, needPhoneNumber: true, sendPhoneNumberToProvider: true);c
