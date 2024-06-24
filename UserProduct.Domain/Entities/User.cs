﻿using Microsoft.AspNetCore.Identity;

namespace UserProduct.Domain.Entities
{
    public class User : IdentityUser, IAuditable
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<Product>? Product { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }


        public static User Create(string firstName, string lastName, string email)
        {
            return new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }
    }
}
