using HotelReservationSystem.Models;
using HotelReservationSystem.Models.Enums;
using HotelReservationSystem.Services;
using HotelReservationSystem.Beauty;

var userService = new UserService();
var roomService = new HotelRoomService();
var reservationService = new ReservationService();

Painter.Title("Welcome to Hotel Reservation System");

bool running = true;
while (running)
{
    try
    {
        Painter.Line();
        Console.WriteLine("1️-Login");
        Console.WriteLine("2️-Register");
        Console.WriteLine("0️-Exit");
        Console.Write("Select: ");
        var choice = Console.ReadLine();
        Console.Clear();
        if (choice == "1")
        {
            Console.Write("Username: ");
            var username = Console.ReadLine()!;
            Console.Write("Password: ");
            var password = Console.ReadLine()!;

            var user = userService.Login(username, password);
            if (user == null)
            {
                Painter.Error("Invalid username or password!");
                continue;
            }

            Painter.Success($"Welcome, {user.Username} ({user.Role})!");
            Console.ReadKey();
            Console.Clear();
            bool loggedIn = true;

            while (loggedIn)
            {
                try
                {
                    switch (user.Role)
                    {
                        case UserRole.Admin:
                            Painter.Title("Admin Menu");
                            Console.WriteLine("1️-  Add Room");
                            Console.WriteLine("2️-  View All Rooms");
                            Console.WriteLine("3️-  View All Reservations");
                            Console.WriteLine("0️-  Logout");
                            Console.Write("Select: ");
                            var adminChoice = Console.ReadLine();
                            Console.Clear();
                            if (adminChoice == "1")
                            {
                                Console.Write("Room Number: ");
                                var num = Console.ReadLine()!;
                                Console.Write("Capacity: ");
                                int cap = int.Parse(Console.ReadLine()!);
                                Console.Write("Price per Night: ");
                                int price = int.Parse(Console.ReadLine()!);
                                Console.Write("Description: ");
                                var desc = Console.ReadLine()!;
                                Console.Write("Has WiFi (y/n): ");
                                bool wifi = Console.ReadLine()!.ToLower() == "y";
                                Console.Write("Has Air Conditioner (y/n): ");
                                bool ac = Console.ReadLine()!.ToLower() == "y";
                                Console.Clear();
                                roomService.AddRoom(num, cap, price, desc, wifi, ac);
                            }
                            else if (adminChoice == "2")
                            {
                                var rooms = roomService.GetAllRooms();
                                Painter.Title("All Rooms");
                                foreach (var r in rooms)
                                    Console.WriteLine($"#{r.Id} | {r.RoomNumber} | {r.Capacity}p | ${r.PricePerNight} | WiFi:{r.RoomDetail?.HasWifi} | AC:{r.RoomDetail?.HasAirConditioner}");
                            }
                            else if (adminChoice == "3")
                            {
                                var res = reservationService.GetAllReservations();
                                Painter.Title("All Reservations");
                                foreach (var r in res)
                                    Console.WriteLine($"#{r.Id} | User:{r.User?.Username} | Room:{r.Room?.RoomNumber} | {r.CheckInDate:d}-{r.CheckOutDate:d} | {r.Status}");
                            }
                            else if (adminChoice == "0")
                            {
                                loggedIn = false;
                                Painter.Info("Logged out.");
                            }
                            else Painter.Error("Invalid option!");
                            break;

                        case UserRole.Receptionist:
                            Painter.Title("Receptionist Menu");
                            Console.WriteLine("1️-  Create Reservation");
                            Console.WriteLine("2️-  Confirm Reservation");
                            Console.WriteLine("3️-  Cancel Reservation");
                            Console.WriteLine("4️-  View All Reservations");
                            Console.WriteLine("0️-  Logout");
                            Console.Write("Select: ");
                            var recChoice = Console.ReadLine();

                            if (recChoice == "1")
                            {
                                var rooms = roomService.GetAllRooms();
                                Painter.Title("Available Rooms");
                                foreach (var r in rooms)
                                    Console.WriteLine($"#{r.Id} | {r.RoomNumber} | ${r.PricePerNight}");

                                Console.Write("Enter User ID: ");
                                int uid = int.Parse(Console.ReadLine()!);
                                Console.Write("Enter Room ID: ");
                                int rid = int.Parse(Console.ReadLine()!);
                                Console.Write("Check-in (yyyy-MM-dd): ");
                                DateTime inDate = DateTime.Parse(Console.ReadLine()!);
                                Console.Write("Check-out (yyyy-MM-dd): ");
                                DateTime outDate = DateTime.Parse(Console.ReadLine()!);
                                Console.Clear();
                                reservationService.CreateReservation(uid, rid, inDate, outDate);
                            }
                            else if (recChoice == "2")
                            {
                                var res = reservationService.GetAllReservations();
                                foreach (var r in res)
                                    Console.WriteLine($"#{r.Id} | {r.Status} | Room:{r.Room?.RoomNumber}");
                                Console.Write("Enter Reservation ID: ");
                                int rid = int.Parse(Console.ReadLine()!);
                                reservationService.ConfirmReservation(rid);
                            }
                            else if (recChoice == "3")
                            {
                                var res = reservationService.GetAllReservations();
                                foreach (var r in res)
                                    Console.WriteLine($"#{r.Id} | {r.Status} | Room:{r.Room?.RoomNumber}");
                                Console.Write("Enter Reservation ID: ");
                                int rid = int.Parse(Console.ReadLine()!);
                                reservationService.CancelReservation(rid);
                            }
                            else if (recChoice == "4")
                            {
                                var res = reservationService.GetAllReservations();
                                Painter.Title("All Reservations");
                                foreach (var r in res)
                                    Console.WriteLine($"#{r.Id} | User:{r.User?.Username} | Room:{r.Room?.RoomNumber} | {r.CheckInDate:d}-{r.CheckOutDate:d} | {r.Status}");
                            }
                            else if (recChoice == "0")
                            {
                                loggedIn = false;
                                Painter.Info("Logged out.");
                            }
                            else Painter.Error("Invalid option!");
                            break;

                        case UserRole.NormalUser:
                            Painter.Title("User Menu");
                            Console.WriteLine("1️-  View My Reservations");
                            Console.WriteLine("0️-  Logout");
                            Console.Write("Select: ");
                            var userChoice = Console.ReadLine();

                            if (userChoice == "1")
                            {
                                var resList = reservationService.GetReservationsByUser(user.Id);
                                if (resList.Count == 0)
                                    Painter.Info("You have no reservations yet.");
                                else
                                {
                                    Painter.Title("Your Reservations");
                                    foreach (var r in resList)
                                        Console.WriteLine($"#{r.Id} | Room:{r.Room?.RoomNumber} | {r.CheckInDate:d}-{r.CheckOutDate:d} | {r.Status}");
                                }
                            }
                            else if (userChoice == "0")
                            {
                                loggedIn = false;
                                Painter.Info("Logged out.");
                            }
                            else Painter.Error("Invalid option!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Painter.Error($"Operation failed: {ex.Message}");
                }
            }
        }
        else if (choice == "2")
        {
            Console.Write("Choose username: ");
            string username = Console.ReadLine()!;
            Console.Write("Choose password: ");
            string password = Console.ReadLine()!;
            Console.WriteLine("Select role (1=Admin, 2=Receptionist, 3=NormalUser): ");
            string roleInput = Console.ReadLine()!;
            Console.Clear();
            UserRole role = roleInput switch
            {
                "1" => UserRole.Admin,
                "2" => UserRole.Receptionist,
                _ => UserRole.NormalUser
            };
            userService.Register(username, password, role);
        }
        else if (choice == "0")
        {
            running = false;
            Painter.Info("Goodbye!");
        }
        else
        {
            Painter.Error("Invalid choice!");
        }
    }
    catch (Exception ex)
    {
        Painter.Error($"Unexpected error: {ex.Message}");
    }
}

