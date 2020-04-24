using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Save_A_Soul.Contexts;
using System;
using System.Linq;

namespace Save_A_Soul.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<Context>>()))
            {
                // Look for any movies.
                if (context.Animals.Any())
                {
                    return;   // DB has been seeded
                }
                context.Shelters.Add(
                    new Shelter
                    {
                        Id = 1,
                        Name = "TimofteHome"
                    }
                    );
                context.Animals.AddRange(
                    new Animal
                    {
                        Name = "Pussy",
                        Weight = 1.23F,
                        Age = 1,
                        Breed = "brazilia",
                        Shelter = new Shelter
                        {
                            Id = 1,
                            Name = "TimofteHome"
                        }
                    },

                    new Animal
                    {
                        Name = "gat",
                        Weight = 1.233F,
                        Age = 12,
                        Breed = "brazilia",
                        Shelter = new Shelter
                        {
                            Id = 1,
                            Name = "TimofteHome"
                        }
                    },

                    new Animal
                    {
                        Name = "Azorel",
                        Weight = 3.98F,
                        Age = 1,
                        Breed = "big boy",
                        Shelter = new Shelter
                        {
                            Id = 1,
                            Name = "TimofteHome"
                        }
                    },

                    new Animal
                    {
                        Name = "Zdreanta cel cu ochy de faiantza",
                        Weight = 2F,
                        Age = 3,
                        Breed = "smol boy",
                        Shelter = new Shelter
                        {
                            Id = 1,
                            Name = "TimofteHome"
                        }
                    }
                ) ;
                context.SaveChanges();
            }
        }
    }
}