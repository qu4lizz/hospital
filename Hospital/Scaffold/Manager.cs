using System;
using System.Collections.Generic;

namespace Hospital.Scaffold
{
    public partial class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Theme { get; set; } = null!;
        public string Language { get; set; } = null!;
    }
}
