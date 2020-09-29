using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcWebsite.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcWebsiteContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcWebsiteContext>>()))
            {
                // Look for any movies.
                if (!context.Board.Any())
                {
                    context.Board.AddRange(
                    new Board
                    {
                        Title = "Test Board 01",
                        CreatedAt = DateTime.Parse("1989-2-12"),
                        Text = "This is a test board 01",
                        Tags = "",
                        BoardColor = "white",
                        Stikies = new List<Stiky>()
                    },

                    new Board
                    {
                        Title = "Test Board 02",
                        CreatedAt = DateTime.Parse("1990-2-12"),
                        Text = "This is a test board 02",
                        Tags = "",
                        BoardColor = "yellow",
                        Stikies = new List<Stiky>()
                    },

                    new Board
                    {
                        Title = "Test Board 03",
                        CreatedAt = DateTime.Parse("2001-2-12"),
                        Text = "This is a test board 03",
                        Tags = "",
                        BoardColor = "blue",
                        Stikies = new List<Stiky>()
                    },

                    new Board
                    {
                        Title = "Test Board 04",
                        CreatedAt = DateTime.Parse("2010-2-12"),
                        Text = "This is a test board 04",
                        Tags = "",
                        BoardColor = "green",
                        Stikies = new List<Stiky>()
                    },

                    new Board
                    {
                        Title = "Test Board 05",
                        CreatedAt = DateTime.Parse("1989-2-12"),
                        Text = "This is a test board 05",
                        Tags = "",
                        BoardColor = "pink",
                        Stikies = new List<Stiky>()
                    },

                    new Board
                    {
                        Title = "Test Board 06",
                        CreatedAt = DateTime.Parse("1990-2-12"),
                        Text = "This is a test board 06",
                        Tags = "",
                        BoardColor = "blue",
                        Stikies = new List<Stiky>()
                    },

                    new Board
                    {
                        Title = "Test Board 07",
                        CreatedAt = DateTime.Parse("2001-2-12"),
                        Text = "This is a test board 07",
                        Tags = "",
                        BoardColor = "pink",
                        Stikies = new List<Stiky>() {
                            new Stiky
                            {
                                CreatedAt = DateTime.Parse("2010-2-12"),
                                Text = "Stiky 03"
                            },
                            new Stiky
                            {
                                CreatedAt = DateTime.Parse("2010-2-12"),
                                Text = "Stiky 04"
                            }
                        }
                    },

                    new Board
                    {
                        Title = "Test Board 08",
                        CreatedAt = DateTime.Parse("2010-2-12"),
                        Text = "This is a test board 08",
                        Tags = "",
                        BoardColor = "green",
                        Stikies = new List<Stiky>() {
                            new Stiky
                            {
                                CreatedAt = DateTime.Parse("2010-2-12"),
                                Text = "Stiky 01"
                            },
                            new Stiky
                            {
                                CreatedAt = DateTime.Parse("2010-2-12"),
                                Text = "Stiky 02"
                            }
                        }
                        
                    }

                );
                    context.SaveChanges();
                }

                if (!context.Stiky.Any())
                {
                    context.Stiky.AddRange(

                        new Stiky
                        {
                            CreatedAt = DateTime.Parse("2010-2-12"),
                            Text = "Stiky 01",
                            BoardId = 0
                        },

                        new Stiky
                        {
                            CreatedAt = DateTime.Parse("2010-2-12"),
                            Text = "Stiky 02",
                            BoardId = 0
                        },

                        new Stiky
                        {
                            CreatedAt = DateTime.Parse("2010-2-12"),
                            Text = "Stiky 03",
                            BoardId = 0
                        }

                    );
                    context.SaveChanges();
                }

                return;   // DB has been seeded


            }
        }
    }
}