using System;
using System.Linq;
using EscapeRoomPlanner.Data.EntityFramework.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EscapeRoomPlanner.Data.EntityFramework
{
    public interface IDataSeeder
    {
        void SeedData();
    }

    public class DataSeeder : IDataSeeder
    {
        private readonly IServiceProvider _Services;


        public DataSeeder(IServiceProvider services)
        {
            _Services = services;
        }

        public void SeedData()
        {
            using (var scope = _Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                loadData(db);

                db.SaveChanges();
            }
        }

        private static void loadData(ApplicationDbContext db)
        {
            string roomNames = "Living Room, Family Room, Formal Living Room, Kitchen, Pantry, Nook, Dining Room, Formal Dining, Closet, Walk-In Closet, Dressing Room, Bedroom, Nursery, Guest Bedroom, Guest Suite, Master Bath Room, Bath Room, Walk-In Shower, Sauna, Three-Quarter Bath, Half-Bath, Mud Room, Sitting Room, Coat Closet, Utility Room, Laundry Room, Sewing Room, Storage Room, Mechanical Room, Garage, RV Garage, Work Shop, Home Gym, Home Office, Study, Library, Drawing Room, Reading Room, Retreat, Den, Parlor, Game Room, Play Room, Media Room, Wet Bar, Butler's/Maid's Quarters, Butler's Kitchen, Butler's Pantry, Wine Cellar, Atrium, Lounge, Gallery, Kitchenette, Attic, Basement, Indoor Pool, Indoor Spa, Stair Tower, Porch, Deck, Garden, Outdoor Kitchen, Outdoor Bar, Outdoor Nook, Gazebo, Green House, Tool Shed, Pool, Spa, Fire Pit, Private Garden, Lawn, Bar-B-Que";

            string roomDescriptions = "Our king size four poster provides views over landscaped gardens. It has a seating area, ample storage, digital safe, minibar and luxurious duck down bedding. Our Deluxe Twin/Large Double also provides views over landscaped gardens. It has a seating area, digital safe, minibar and luxurious duck down bedding. This room can be configured with either 2 single beds or zip and linked to provide a large double bed. As our smallest budget rooms, the Compact bedrooms are suited for single occupancy or short-stay double occupancy as they have limited space and storage. All our guestrooms are elegantly furnished with handmade furniture include luxury en-suite facilities with complimentary amenities pack, flat screen LCD TV, tea/coffee making facilities, fan, hairdryer and the finest pure white linen and towels. Rooms were spacious, comfortable and spotlessly clean and free wi-fi. We had the four poster room which had lovely views over the immaculate gardens.";

            var random = new Random();

            var names = roomNames.Split(",");

            var numberOfrooms = random.Next(50);

            for (int i = 0; i < numberOfrooms; i++)
            {
                var times = random.Next(24);
                var descriptions = roomDescriptions.Split(".")
                    .OrderBy(item => random.Next(roomDescriptions.Length))
                    .Take(random.Next(1, 4))
                    .ToList();

                var room = new Room
                {
                    Name = names[random.Next(names.Length - 1)],
                    Description = string.Concat(descriptions),
                    OpeningTime = times,
                    ClosingTime = times + 1,
                };

                db.Add(room);
            }
        }
    }
}