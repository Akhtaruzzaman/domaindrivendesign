using Common;
using System;

namespace Domain
{
    public class Users: Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
