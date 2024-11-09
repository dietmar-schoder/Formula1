﻿namespace Formula1.Domain.Entities;

public class Race
{
    public Guid Id { get; set; }

    public int SeasonYear { get; set; }
    public Season Season { get; set; }

    public int Round { get; set; }

    public string WikipediaUrl { get; set; }

    public Guid CircuitId { get; set; }
    public Circuit Circuit { get; set; }

    public Guid GrandPrixId { get; set; }
    public GrandPrix GrandPrix { get; set; }

    public ICollection<Session> Sessions { get; set; }
}
