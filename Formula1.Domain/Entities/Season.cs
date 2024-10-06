﻿namespace Formula1.Domain.Entities
{
    public class Season
    {
        public int Year { get; set; }

        public string WikipediaUrl { get; set; }

        public ICollection<Race> Races { get; set; }
    }
}
