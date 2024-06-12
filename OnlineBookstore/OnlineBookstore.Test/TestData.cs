using OnlineBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Test
{
    public static class TestData
    {
        public static List<Book> GetBooks()
        {
            return new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Genre = "Genre 1", ISBN = "1234567890", Author = "Author 1", PublicationYear = 2021, Price = 10.0m, Description = "Description 1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Book { Id = 2, Title = "Book 2", Genre = "Genre 2", ISBN = "0987654321", Author = "Author 2", PublicationYear = 2022, Price = 15.0m, Description = "Description 2", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
        };
        }

        public static List<AppUser> GetUsers()
        {
            return new List<AppUser>
        {
            new AppUser { Id = 1, Username = "User1", Email = "user1@example.com", PasswordHash = "hash1", FirstName = "First1", LastName = "Last1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new AppUser { Id = 2, Username = "User2", Email = "user2@example.com", PasswordHash = "hash2", FirstName = "First2", LastName = "Last2", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
        };
        }

        public static List<CartItem> GetCartItems()
        {
            return new List<CartItem>
        {
            new CartItem { Id = 1, UserId = 1, BookId = 1, Quantity = 1, AddedAt = DateTime.Now },
            new CartItem { Id = 2, UserId = 1, BookId = 2, Quantity = 2, AddedAt = DateTime.Now }
        };
        }

        public static List<PurchaseHistory> GetPurchaseHistories()
        {
            return new List<PurchaseHistory>
        {
            new PurchaseHistory
            {
                Id = 1, UserId = 1, PurchaseDate = DateTime.Now, TotalAmount = 25.0m,
                Items = new List<PurchaseHistoryItem>
                {
                    new PurchaseHistoryItem { Id = 1, PurchaseHistoryId = 1, BookId = 1, Quantity = 1, Price = 10.0m },
                    new PurchaseHistoryItem { Id = 2, PurchaseHistoryId = 1, BookId = 2, Quantity = 1, Price = 15.0m }
                }
            }
        };
        }
    }


}
