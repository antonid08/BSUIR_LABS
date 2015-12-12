using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class Composition
    {
        public enum Genres
        {
            Rock,
            Rap,
            Pop,
            Country,
            Jazz,
            No_genre
        }

        public int Id { get; set; }
        public int Rating { get; set; }

        public string Name { get; set; }
        public string Performer { get; set; }

        public TimeSpan Length { get; set; }
        public Genres Genre { get; set; }

        public Composition(int id, int rating, string name, string performer,
                            TimeSpan length)
        {
            Id = id;
            Rating = rating;
            Name = name;
            Performer = performer;

            Length = length;
        }

    }
}
