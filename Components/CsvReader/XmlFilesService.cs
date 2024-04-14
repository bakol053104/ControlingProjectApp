using System.Xml.Linq;

namespace ControlingProjectApp.Components.CsvReader;

public class XmlFilesService : IXmlFilesService
{
    private readonly ICsvReader _csvReader;
    public XmlFilesService(ICsvReader csvReader)
    {
        _csvReader = csvReader;
    }
    public void CreateXml(string csvFilePath, string xmlFilePath)
    {
        var carslist = _csvReader.ProcessCar(csvFilePath);

        var document = new XDocument();
        var cars = new XElement("Cars", carslist
            .Select(x =>
                new XElement("Car",
                    new XAttribute("Name", x.Name!),
                    new XAttribute("Combined", x.Combined),
                    new XAttribute("Manufacturer", x.Manufacturer!))));

        document.Add(cars);
        document.Save(xmlFilePath);
    }

    public List<string> QueryXml(string csvFilePath)
    {
        var document = XDocument.Load(csvFilePath);
        var names = document
            .Element("Cars")?
            .Elements("Car")
            .Where(x => x.Attribute("Manufacturer")?.Value == "BMW")
            .Select(x => x.Attribute("Name")?.Value)
            .ToList();

        return names!;
    }

    public void InquiryXmlTask(string fuelFilePath, string manufacturerFilePath, string xmlTaskFilePath)
    {
        var carlist = _csvReader.ProcessCar(fuelFilePath);
        var manufacturerlist = _csvReader.ProcessManufacturers(manufacturerFilePath);

        var groups = manufacturerlist.GroupJoin(
            carlist,
            manufacturer => manufacturer.Name,
            car => car.Manufacturer,
            (m, g) =>
                new
                {
                    Manufacturer = m,
                    Cars = g
                }
            )
        .OrderBy(x => x.Manufacturer.Name);

        var document = new XDocument();
        var manufacturers = new XElement("Manufacturers", groups
            .Select(m =>
                new XElement("Manufacturer",
                    new XAttribute("Name", m.Manufacturer.Name!),
                    new XAttribute("Country", m.Manufacturer.Country!),
                        new XElement("Cars",
                            new XAttribute("Country", m.Manufacturer.Country!),
                            new XAttribute("CombinedSum", m.Cars.Sum(c => c.Combined)),
                                new XElement("Car", m.Cars
                                    .Select(g =>
                                   new XElement("Car",
                                       new XAttribute("Model", g.Name!),
                                       new XAttribute("Combined", g.Combined))))))));

        document.Add(manufacturers);
        document.Save(xmlTaskFilePath);
    }
}
