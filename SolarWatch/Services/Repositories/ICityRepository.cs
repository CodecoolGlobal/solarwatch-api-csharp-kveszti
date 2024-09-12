using System.Threading.Tasks;
using SolarWatch.Model;

namespace SolarWatch.Services.Repositories;

public interface ICityRepository
{
    public Task<IEnumerable<City>> GetAllCities();
    public Task<City?> GetByName(string name);
    public Task<City?> GetById(int id);

    public Task Add(City city);
    
    public Task Delete(int id);

    public Task Update(City city);
}
