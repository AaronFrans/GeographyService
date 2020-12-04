using System;

namespace DomainLayer
{
    public class Continent
    {
        public int Id { get; set;}
        public string Name { get; private set; }
        public int Population { get; private set; }
    }
}
