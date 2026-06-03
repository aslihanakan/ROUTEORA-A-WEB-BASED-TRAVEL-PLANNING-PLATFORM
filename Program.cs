using Microsoft.EntityFrameworkCore;
using TravelPlanner.Data;
using TravelPlanner.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout        = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly    = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- SEED DATA ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Destinations.Any())
    {
        db.Destinations.AddRange(
            // Japan
            new Destination { CountryName = "Japan",       CityName = "Tokyo",        Description = "Modern metropolis blending ancient temples with futuristic technology and neon-lit streets.",       ImageUrl = "/images/destinations/Japan/Tokyo.jpg",                   BestSeason = "Spring",  EstimatedBudget = 1800 },
            new Destination { CountryName = "Japan",       CityName = "Osaka",        Description = "Japan's kitchen — a paradise for food lovers with vibrant nightlife and friendly locals.",          ImageUrl = "/images/destinations/Japan/Osaka.jpg",                   BestSeason = "Spring",  EstimatedBudget = 1600 },
            new Destination { CountryName = "Japan",       CityName = "Kyoto",        Description = "Japan's cultural heart with over 1600 Buddhist temples, Zen gardens and geisha districts.",        ImageUrl = "/images/destinations/Japan/Kyoto.jpg",                   BestSeason = "Spring",  EstimatedBudget = 1500 },
            // Italy
            new Destination { CountryName = "Italy",       CityName = "Rome",         Description = "The Eternal City — two thousand years of history, art and world-famous cuisine around every corner.", ImageUrl = "/images/destinations/Italy/Rome.jpg",                  BestSeason = "Autumn",  EstimatedBudget = 1400 },
            new Destination { CountryName = "Italy",       CityName = "Venice",       Description = "Floating city of canals, gondolas, carnival masks and breathtaking Gothic architecture.",           ImageUrl = "/images/destinations/Italy/Venice.jpg",                  BestSeason = "Spring",  EstimatedBudget = 1600 },
            new Destination { CountryName = "Italy",       CityName = "Milan",        Description = "Italy's fashion and design capital, home to Da Vinci's Last Supper and world-class shopping.",    ImageUrl = "/images/destinations/Italy/Milano.jpg",                  BestSeason = "Spring",  EstimatedBudget = 1500 },
            // Spain
            new Destination { CountryName = "Spain",       CityName = "Barcelona",    Description = "Gaudí's architectural masterpieces, golden beaches and a buzzing tapas and nightlife scene.",     ImageUrl = "/images/destinations/Spain/Barcelona.jpg",               BestSeason = "Summer",  EstimatedBudget = 1300 },
            new Destination { CountryName = "Spain",       CityName = "Madrid",       Description = "Spain's vibrant capital packed with world-class art museums, royal palaces and lively plazas.",   ImageUrl = "/images/destinations/Spain/Madrid.jpg",                  BestSeason = "Spring",  EstimatedBudget = 1200 },
            new Destination { CountryName = "Spain",       CityName = "Seville",      Description = "Flamenco, orange blossoms and the magnificent Alcázar palace — Andalusia at its finest.",         ImageUrl = "/images/destinations/Spain/Sevilla.jpg",                 BestSeason = "Spring",  EstimatedBudget = 1100 },
            // Korea
            new Destination { CountryName = "Korea",       CityName = "Seoul",        Description = "K-pop, kimchi, high-tech streets and centuries-old palaces coexist in this dynamic capital.",    ImageUrl = "/images/destinations/Korea/Seoul.jpg",                   BestSeason = "Autumn",  EstimatedBudget = 1300 },
            new Destination { CountryName = "Korea",       CityName = "Busan",        Description = "South Korea's coastal gem with colorful hillside villages, beaches and fresh seafood markets.",   ImageUrl = "/images/destinations/Korea/Busan.jpg",                   BestSeason = "Summer",  EstimatedBudget = 1100 },
            new Destination { CountryName = "Korea",       CityName = "Gyeongju",     Description = "Open-air museum city dotted with royal tombs, ancient temples and Silla-era treasures.",         ImageUrl = "/images/destinations/Korea/Gyeongju.jpg",                BestSeason = "Autumn",  EstimatedBudget = 900  },
            // Thailand
            new Destination { CountryName = "Thailand",    CityName = "Bangkok",      Description = "Vibrant street food, golden temples and electric nightlife in Southeast Asia's buzzing hub.",    ImageUrl = "/images/destinations/Thailand/Bangkok.jpg",              BestSeason = "Winter",  EstimatedBudget = 900  },
            new Destination { CountryName = "Thailand",    CityName = "Chiang Mai",   Description = "Northern gem surrounded by misty mountains, weekly night markets and elephant sanctuaries.",     ImageUrl = "/images/destinations/Thailand/Chiang Mai.jpg",           BestSeason = "Winter",  EstimatedBudget = 700  },
            new Destination { CountryName = "Thailand",    CityName = "Phuket",       Description = "Thailand's largest island with turquoise bays, coral reefs and legendary beach parties.",        ImageUrl = "/images/destinations/Thailand/Phuket.jpg",               BestSeason = "Winter",  EstimatedBudget = 800  },
            // Russia
            new Destination { CountryName = "Russia",      CityName = "Moscow",       Description = "Red Square, the Kremlin and world-class ballet in Russia's magnificent imperial capital.",       ImageUrl = "/images/destinations/Russia/Moskow.jpg",                 BestSeason = "Summer",  EstimatedBudget = 1200 },
            new Destination { CountryName = "Russia",      CityName = "St. Petersburg", Description = "The Venice of the North — baroque palaces, the Hermitage and endless white nights.",          ImageUrl = "/images/destinations/Russia/Petersburg.jpg",             BestSeason = "Summer",  EstimatedBudget = 1100 },
            new Destination { CountryName = "Russia",      CityName = "Novosibirsk",  Description = "Gateway to Siberia with a vibrant arts scene, the vast Ob reservoir and Trans-Siberian railway.", ImageUrl = "/images/destinations/Russia/Novosibirsk.jpg",          BestSeason = "Summer",  EstimatedBudget = 950  },
            // Switzerland
            new Destination { CountryName = "Switzerland", CityName = "Zurich",       Description = "Switzerland's financial heart with a charming old town, pristine lake and world-class dining.", ImageUrl = "/images/destinations/Switzerland/Zurich.jpg",             BestSeason = "Summer",  EstimatedBudget = 2500 },
            new Destination { CountryName = "Switzerland", CityName = "Zermatt",      Description = "Car-free alpine village beneath the iconic Matterhorn peak — skiing and hiking paradise.",      ImageUrl = "/images/destinations/Switzerland/Zermatt.jpg",           BestSeason = "Winter",  EstimatedBudget = 2800 },
            new Destination { CountryName = "Switzerland", CityName = "Bern",         Description = "Switzerland's medieval capital with arcaded streets, rose gardens and bear park.",              ImageUrl = "/images/destinations/Switzerland/Bern.jpg",              BestSeason = "Summer",  EstimatedBudget = 2200 },
            // Türkiye
            new Destination { CountryName = "Türkiye",     CityName = "Istanbul",     Description = "Where Europe meets Asia — mosques, bazaars, Bosphorus cruises and legendary street food.",       ImageUrl = "/images/destinations/Turkiye/Istanbul.jpg",              BestSeason = "Spring",  EstimatedBudget = 1000 },
            new Destination { CountryName = "Türkiye",     CityName = "Cappadocia",   Description = "Surreal fairy chimneys, hot air balloons at dawn and ancient underground cities.",               ImageUrl = "/images/destinations/Turkiye/Cappadocia.jpg",            BestSeason = "Spring",  EstimatedBudget = 900  },
            new Destination { CountryName = "Türkiye",     CityName = "Trabzon",      Description = "Black Sea gem with lush highlands, the clifftop Sümela Monastery and fresh anchovy cuisine.",   ImageUrl = "/images/destinations/Turkiye/Trabzon.jpg",               BestSeason = "Summer",  EstimatedBudget = 700  }
        );
        db.SaveChanges();
    }

    if (!db.VisaFreeCountries.Any())
    {
        db.VisaFreeCountries.AddRange(
            new VisaFreeCountry { CountryName = "Japan",        VisaDuration = "90 days",  BestCities = "Tokyo, Kyoto, Osaka",           BestSeason = "Spring",  EstimatedBudget = 1800 },
            new VisaFreeCountry { CountryName = "Thailand",     VisaDuration = "30 days",  BestCities = "Bangkok, Chiang Mai, Phuket",    BestSeason = "Winter",  EstimatedBudget = 800  },
            new VisaFreeCountry { CountryName = "Georgia",      VisaDuration = "365 days", BestCities = "Tbilisi, Batumi, Kazbegi",       BestSeason = "Summer",  EstimatedBudget = 600  },
            new VisaFreeCountry { CountryName = "Serbia",       VisaDuration = "30 days",  BestCities = "Belgrade, Novi Sad",             BestSeason = "Summer",  EstimatedBudget = 500  },
            new VisaFreeCountry { CountryName = "Montenegro",   VisaDuration = "90 days",  BestCities = "Kotor, Budva, Podgorica",        BestSeason = "Summer",  EstimatedBudget = 700  },
            new VisaFreeCountry { CountryName = "Bosnia",       VisaDuration = "90 days",  BestCities = "Sarajevo, Mostar",               BestSeason = "Summer",  EstimatedBudget = 550  },
            new VisaFreeCountry { CountryName = "Malaysia",     VisaDuration = "90 days",  BestCities = "Kuala Lumpur, Penang, Langkawi", BestSeason = "Winter",  EstimatedBudget = 750  },
            new VisaFreeCountry { CountryName = "Indonesia",    VisaDuration = "30 days",  BestCities = "Bali, Yogyakarta, Lombok",       BestSeason = "Summer",  EstimatedBudget = 850  },
            new VisaFreeCountry { CountryName = "South Korea",  VisaDuration = "90 days",  BestCities = "Seoul, Busan, Jeju",             BestSeason = "Autumn",  EstimatedBudget = 1200 },
            new VisaFreeCountry { CountryName = "Morocco",      VisaDuration = "90 days",  BestCities = "Marrakech, Fes, Chefchaouen",    BestSeason = "Spring",  EstimatedBudget = 750  }
        );
        db.SaveChanges();
    }

    if (!db.SuggestedRoutes.Any())
    {
        db.SuggestedRoutes.AddRange(
            new SuggestedRoute { RouteName = "Spring Asia Escape",     Countries = "Japan → South Korea → Thailand", Duration = "21 days", BudgetLevel = "Mid-range", Description = "Cherry blossoms in Tokyo, K-pop culture in Seoul, and beach relaxation in Thailand. The perfect spring trilogy." },
            new SuggestedRoute { RouteName = "European Budget Trip",   Countries = "Serbia → Bosnia → Montenegro",   Duration = "14 days", BudgetLevel = "Budget",    Description = "Explore the hidden gems of the Balkans — vibrant nightlife in Belgrade, Ottoman heritage in Mostar, and Adriatic coastline in Kotor." },
            new SuggestedRoute { RouteName = "Mediterranean Summer",   Countries = "Italy → Greece → Croatia",       Duration = "18 days", BudgetLevel = "Mid-range", Description = "Roman history, Santorini sunsets, and Dubrovnik's medieval walls. The ultimate Mediterranean summer journey." },
            new SuggestedRoute { RouteName = "Southeast Asia Classic", Countries = "Thailand → Malaysia → Indonesia", Duration = "24 days", BudgetLevel = "Budget",   Description = "Street food in Bangkok, colonial charm of Penang, and the spiritual magic of Bali. A backpacker's dream route." },
            new SuggestedRoute { RouteName = "Iberian Adventure",      Countries = "Portugal → Morocco",              Duration = "12 days", BudgetLevel = "Mid-range", Description = "Lisbon's trams and pastéis de nata, then cross to Marrakech's bustling souks and Saharan landscapes." },
            new SuggestedRoute { RouteName = "Caucasus Explorer",      Countries = "Georgia → Armenia → Azerbaijan",  Duration = "16 days", BudgetLevel = "Budget",    Description = "Tbilisi's old town, ancient monasteries of Armenia, and the fire temples of Baku. Undiscovered and unforgettable." }
        );
        db.SaveChanges();
    }

    if (!db.TravelTips.Any())
    {
        db.TravelTips.AddRange(
            new TravelTip { Title = "Packing Tips",         Content = "Roll your clothes instead of folding to save space. Use packing cubes to stay organised, and always pack a light rain jacket regardless of destination." },
            new TravelTip { Title = "Budget Travel Advice", Content = "Book flights 6–8 weeks ahead for the best prices. Use local markets instead of tourist restaurants, and consider overnight trains to save on accommodation." },
            new TravelTip { Title = "Safety Tips",          Content = "Keep a digital copy of your passport in cloud storage. Share your itinerary with a trusted contact, and use a money belt in crowded areas." },
            new TravelTip { Title = "Travel Essentials",    Content = "A universal adapter, portable charger, reusable water bottle, and noise-cancelling headphones will make any long trip significantly more comfortable." },
            new TravelTip { Title = "Best Booking Times",   Content = "Tuesday and Wednesday are statistically the cheapest days to book flights. Set price alerts on Google Flights and be flexible with your travel dates." },
            new TravelTip { Title = "Local Etiquette",      Content = "Research basic customs before you arrive — dress codes for temples, tipping culture, and common gestures that may have different meanings abroad." }
        );
        db.SaveChanges();
    }
}
// --- END SEED DATA ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
