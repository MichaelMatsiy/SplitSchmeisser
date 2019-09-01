using SplitSchmeisser.DAL.Context;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Linq;

namespace SplitSchmeisser.DAL
{
    public class DbInitializer
    {
        public static void Initialize(SchmeisserContext context)
        {
            if (context.Groups.Any())
            {
                return;
            }

            var users = new User[] 
            {
                new User {
                    Name = "Admin",
                    Password ="1234"
                },
                new User {
                    Name = "Jhon",
                    Password ="1234"
                },
                new User {
                    Name = "Mark",
                    Password ="1234"
                },
                new User {
                    Name = "Kevin",
                    Password ="1234"
                }
            };

            var groups = new Group[]
            {
                new Group {
                    Name ="Carson",
                    Users = users
                },
                new Group {
                    Name ="Meredith",
                    Users = users
                },
                new Group {
                    Name ="Arturo",
                    Users = users
                },
                new Group {
                    Name ="Gytis",
                    Users = users
                },
                new Group {
                    Name ="Yan",
                    Users = users
                },
                new Group {
                    Name ="Peggy",
                    Users = users
                },
                new Group {
                    Name ="Laura",
                    Users = users
                },
                new Group {
                    Name ="Nino",
                    Users = users
                }
            };

            var operations = new Operation[] {
                new Operation {
                    Group = groups[0],
                    Owner = users[0],
                    Amount = 100,
                    OwnerId = 1,
                    DateOfLoan = DateTime.Today
                },
                new Operation {
                    Group = groups[1],
                    Owner = users[0],
                    Amount = 100,
                    OwnerId = 1,
                    DateOfLoan = DateTime.Today
                },
                new Operation {
                    Group = groups[2],
                    Owner = users[0],
                    Amount = 100,
                    OwnerId = 1,
                    DateOfLoan = DateTime.Today
                },
                new Operation {
                    Group = groups[3],
                    Owner = users[0],
                    Amount = 100,
                    OwnerId = 1,
                    DateOfLoan = DateTime.Today
                },
                new Operation {
                    Group = groups[4],
                    Owner = users[0],
                    Amount = 100,
                    OwnerId = 1,
                    DateOfLoan = DateTime.Today
                },
                new Operation {
                    Group = groups[5],
                    Owner = users[0],
                    Amount = 100,
                    OwnerId = 1,
                    DateOfLoan = DateTime.Today
                },
                new Operation {
                    Group = groups[6],
                    Owner = users[0],
                    Amount = 100,
                    OwnerId = 1,
                    DateOfLoan = DateTime.Today
                },
                new Operation {
                    Group = groups[7],
                    Owner = users[0],
                    Amount = 100,
                    OwnerId = 1,
                    DateOfLoan = DateTime.Today
                }
            };

            context.Groups.AddRange(groups);
            context.Users.AddRange(users);
            context.Operations.AddRange(operations);

            context.SaveChanges();
        }
    }
}
