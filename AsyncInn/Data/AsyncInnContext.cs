using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace AsyncInn.Data
{
    public class AsyncInnContext : DbContext
    {
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<RoomAmens> RoomAmens { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }


        public AsyncInnContext(DbContextOptions<AsyncInnContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {




        }

    }
}


