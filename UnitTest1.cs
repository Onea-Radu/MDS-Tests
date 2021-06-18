using EmagClone.Entities;
using EmagClone.Services;
using Microsoft.EntityFrameworkCore;
using OldIronIronWeTake.Data;
using System;
using System.Linq;
using Xunit;


namespace MDS_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void DatabaseTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test1");
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {

                var user = new User { Email = "bogdan@gmail.com" };
                context.Users.Add(user);
                context.SaveChanges();
                Assert.True(context.Users.Any());
            }
        }

        [Fact]
        public void ProductTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test2");
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {

                var user = new User { Email = "bogdan@gmail.com" };
                context.Users.Add(user);
                context.SaveChanges();
                Assert.True(context.Users.Any());

                var product = new Product { Name = "Banane", Seller = user, Stock = 12 };
                var service = new ProductService(context);
                service.Post(product);
                Assert.Contains(product, service.GetAll());
                service.Delete(1);
                Assert.True(service.GetAll().Count == 0);
            }



        }


        [Fact]
        public void ProblemsTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test3");
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {

                var user = new User { Email = "bogdan@gmail.com" };
                context.Users.Add(user);
                context.SaveChanges();
                Assert.True(context.Users.Any());

                var problem = new Problem { Text = "Nu merge", User = user };
                var service = new ProblemService(context);
                service.Add(problem);
                Assert.Contains(problem, service.GetAll());
                service.Delete(1);
                Assert.True(service.GetAll().Count == 0);
            }



        }


        [Fact]
        public void ReviewTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test4");
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {

                var user = new User { Email = "bogdan@gmail.com" };
                context.Users.Add(user);
                context.SaveChanges();
                Assert.True(context.Users.Any());

                var product = new Product { Name = "Banane", Seller = user, Stock = 12 };
                var service = new ProductService(context);
                service.Post(product);
                Assert.Contains(product, service.GetAll());
                var service2 = new ReviewService(context);
                var review = new Review { Text = "Super", Product = product, User = user };

                service2.Post(review);
                Assert.Contains(review, service2.GetAll());
                service2.Delete(1);
                Assert.True(service2.GetAll().Count == 0);

            }


        }
        [Fact]
        public void FavoriteTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test5");
            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {

                var user = new User { Email = "bogdan@gmail.com" };
                context.Users.Add(user);
                context.SaveChanges();
                Assert.True(context.Users.Any());

                var product = new Product { Name = "Banane", Seller = user, Stock = 12 };
                var service = new ProductService(context);
                service.Post(product);
                Assert.Contains(product, service.GetAll());
                var service2 = new FavoritesService(context);
                service2.AddToFavorites(1, user);
                Assert.Equal(service2.GetAll(user).First().Product, product);
                service2.RemoveFromFavorites(1);
                Assert.True(service2.GetAll(user).Count == 0);

            }


        }
    }
}
