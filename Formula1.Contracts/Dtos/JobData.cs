namespace Formula1.Contracts.Dtos;

public class JobData
{
    public double mean { get; set; }
    public string __CLASS__ { get; set; }
    public int count { get; set; }
    public List<Result> Results { get; set; }
}

public class Category
{
    public string label { get; set; }
    public string __CLASS__ { get; set; }
    public string tag { get; set; }
}

public class Company
{
    public string display_name { get; set; }
    public string __CLASS__ { get; set; }
}

public class Location
{
    public string display_name { get; set; }
    public List<string> area { get; set; }
    public string __CLASS__ { get; set; }
}

public class Result
{
    public string id { get; set; }
    public double latitude { get; set; }
    public Company company { get; set; }
    public string adref { get; set; }
    public string contract_type { get; set; }
    public double salary_min { get; set; }
    public string title { get; set; }
    public DateTime created { get; set; }
    public Location location { get; set; }
    public string salary_is_predicted { get; set; }
    public string __CLASS__ { get; set; }
    public double salary_max { get; set; }
    public double longitude { get; set; }
    public string redirect_url { get; set; }
    public string description { get; set; }
    public Category category { get; set; }
    public string contract_time { get; set; }
}
