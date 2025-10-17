using HotelReservationSystem.Models;
using HotelReservationSystem.Models.Entities;
using HotelReservationSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Data
{
    public class HotelContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomDetail> RoomDetails { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Initial Catalog=Hotel; Integrated Security=True; Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Password).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Role).HasConversion<string>();
            });

            modelBuilder.Entity<HotelRoom>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.RoomNumber).IsRequired().HasMaxLength(20);
                entity.HasIndex(r => r.RoomNumber).IsUnique();
                entity.Property(r => r.PricePerNight).IsRequired();
            });

            modelBuilder.Entity<RoomDetail>(entity =>
            {
                entity.HasKey(rd => rd.RoomId);
                entity.HasOne(rd => rd.Room)
                      .WithOne(r => r.RoomDetail)
                      .HasForeignKey<RoomDetail>(rd => rd.RoomId);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Status).HasConversion<string>();
                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reservations)
                      .HasForeignKey(r => r.UserId);
                entity.HasOne(r => r.Room)
                      .WithMany(h => h.Reservations)
                      .HasForeignKey(r => r.RoomId);
            });
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "admin123", Role = UserRole.Admin, CreatedAt = new DateTime(2024, 1, 1) },
                new User { Id = 2, Username = "reception", Password = "reception123", Role = UserRole.Receptionist, CreatedAt = new DateTime(2024, 1, 2) },
                new User { Id = 3, Username = "user1", Password = "user123", Role = UserRole.NormalUser, CreatedAt = new DateTime(2024, 1, 3) }
            );

            modelBuilder.Entity<HotelRoom>().HasData(
                new HotelRoom { Id = 1, RoomNumber = "101", Capacity = 2, PricePerNight = 100, CreatedAt = new DateTime(2024, 2, 1) },
                new HotelRoom { Id = 2, RoomNumber = "102", Capacity = 3, PricePerNight = 150, CreatedAt = new DateTime(2024, 2, 2) }
            );

            modelBuilder.Entity<RoomDetail>().HasData(
                new RoomDetail { RoomId = 1, Description = "Standard Room", HasWifi = true, HasAirConditioner = true },
                new RoomDetail { RoomId = 2, Description = "Family Room", HasWifi = true, HasAirConditioner = false }
            );

        }
    }
}

