namespace SolarWatch.Model;

public class City
{
    public City(string name, double latitude, double longitude, string country, string state)
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        Country = country;
        State = state;
    }

    public int Id { get; private set; }
    public string Name { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
}