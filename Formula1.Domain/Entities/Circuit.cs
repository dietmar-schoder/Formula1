﻿namespace Formula1.Domain.Entities;

public class Circuit
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string WikipediaUrl { get; set; }

    public virtual ICollection<Race> Races { get; set; }
}
