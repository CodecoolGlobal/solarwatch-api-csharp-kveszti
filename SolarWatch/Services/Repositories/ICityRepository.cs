using System.Threading.Tasks;
using SolarWatch.Model;

namespace SolarWatch.Services.Repositories;

public interface ICityRepository
{
    public Task<IEnumerable<City>> GetAllCities();
    public City? GetByName(string name);
    public City? GetById(int id);

    public void Add(City city);
    
    public Task Delete(int id);

    public Task Update(City city);
}
