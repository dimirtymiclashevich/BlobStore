using System;
using System.Collections.Generic;
using System.Linq;

namespace BlobStore.Models
{
    public static class UserList
    {
        public static List<User> Users { get; }

        static UserList()
        {
            Users = new List<User>
            {
                new User("OLIVER", Guid.Parse("{155B3B0B-5F22-4E95-B923-FB63D07C6235}")),
                new User("JACK", Guid.Parse("{255B3B0B-5F22-4E95-B923-FB63D07C6235}")),
                new User("HARRY", Guid.Parse("{355B3B0B-5F22-4E95-B923-FB63D07C6235}")),
                new User("AMELIA", Guid.Parse("{455B3B0B-5F22-4E95-B923-FB63D07C6235}")),
                new User("EMILY", Guid.Parse("{555B3B0B-5F22-4E95-B923-FB63D07C6235}"))
            };
        }

        public static Guid GetId(string name)
        {
            var user = Users.FirstOrDefault(u => u.Name == name);
            if (user != null)
            {
                return user.Id;
            }
            return Guid.Empty;
        }
    }
}

