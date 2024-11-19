﻿namespace Formula1.Domain.Entities;

public class Driver
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string WikipediaUrl { get; set; }

    public virtual ICollection<Result> Results { get; set; }
}
