using ControlingProjectApp.Components.Models;

namespace ControlingProjectApp.Components.CsvReader;

public interface ICsvReader
{
    List<Car> ProcessCar(string filepath);

    List<Manufacturer> ProcessManufacturers(string filepath);
}
