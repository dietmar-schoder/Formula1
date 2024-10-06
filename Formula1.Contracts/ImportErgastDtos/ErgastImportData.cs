namespace Formula1.Contracts.ImportErgastDtos;

public class ErgastImportData
{
    public MRData MRData { get; set; }
}

public class MRData
{
    public string limit { get; set; }
    public string offset { get; set; }
    public string total { get; set; }
    public SeasonTable SeasonTable { get; set; }
    public CircuitTable CircuitTable { get; set; }
    public RaceTable RaceTable { get; set; }
}

public class Circuit
{
    public string circuitId { get; set; }
    public string url { get; set; }
    public string circuitName { get; set; }
    public Location Location { get; set; }
}

public class CircuitTable
{
    public List<Circuit> Circuits { get; set; }
}

public class Location
{
    public string lat { get; set; }
    public string @long { get; set; }
    public string locality { get; set; }
    public string country { get; set; }
}

public class Race
{
    public string season { get; set; }
    public string round { get; set; }
    public string url { get; set; }
    public string raceName { get; set; }
    public Circuit Circuit { get; set; }
    public string date { get; set; }
}

public class RaceTable
{
    public string season { get; set; }
    public List<Race> Races { get; set; }
}

public class Season
{
    public string season { get; set; }
    public string url { get; set; }
}

public class SeasonTable
{
    public List<Season> Seasons { get; set; }
}
