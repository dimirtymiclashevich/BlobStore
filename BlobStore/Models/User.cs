using System;

namespace BlobStore.Models
{
    public class User
    {
        public string Name { get; }
        public Guid Id { get; }

        public User(string name, Guid id)
        {
            Name = name;
            Id = id;
        }
    }
}
