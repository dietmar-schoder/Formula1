namespace Formula1.Contracts.Dtos;

public class ErgastSeasonsData
{
    public MRData MRData { get; set; }
}

public class MRData
{
    public string xmlns { get; set; }
    public string series { get; set; }
    public string url { get; set; }
    public string limit { get; set; }
    public string offset { get; set; }
    public string total { get; set; }
    public SeasonTable SeasonTable { get; set; }
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
