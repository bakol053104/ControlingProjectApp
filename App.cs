using ControlingProjectApp.Components.CsvReader;
using ControlingProjectApp.Components.Models;
using ControlingProjectApp.Components.ProviderData;
using ControlingProjectApp.Services;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ControlingProjectApp;

public class App : IApp
{
    private readonly IUserMenu _userMenu;
    private readonly IDataProviderSql _dataProviderSql;

    public App(IDataProviderSql dataProviderSql, IUserMenu userMenu)
    {
        _dataProviderSql = dataProviderSql;
        _userMenu = userMenu;
    }

    public void Run()
    {

     

        //var groupsJoin = manufacturers.GroupJoin(
        //    cars,
        //    manufacturer => manufacturer.Name,
        //    car => car.Manufacturer,
        //    (m, g) =>
        //    new
        //    {
        //        Manufacturer = m,
        //        Cars = g,
        //    }
        //    )
        //    .OrderBy(x => x.Manufacturer.Name);
        //foreach (var item in groupsJoin)
        //{
        //    Console.WriteLine($"Manufacturer: {item.Manufacturer.Name}");
        //    Console.WriteLine($"\t Cars: {item.Cars.Count()}");
        //    Console.WriteLine($"\t Max: {item.Cars.Max(x => x.Combined)}");
        //    Console.WriteLine($"\t Min: {item.Cars.Min(x => x.Combined)}");
        //    Console.WriteLine($"\t Avg: {item.Cars.Average(x => x.Combined)}");
        //    Console.WriteLine();
        //}


        //Console.ReadLine();
        //CreateXml(cars);
        //QueryXml();
        //Console.ReadLine();

        _dataProviderSql.AddToDataBaseEmployees();
        _dataProviderSql.AddToDataBaseProjects();
        _userMenu.MainUserMenuSelect();
    }

    //private static void CreateXml(List<Car> cars)
    //{
    //    var document = new XDocument();
    //    var carsXml = new XElement("Cars", cars
    //        .Select(x =>
    //        new XElement("Car",
    //        new XAttribute("Name", x.Name!),
    //        new XAttribute("Combined", x.Combined),
    //        new XAttribute("Manufacturer", x.Manufacturer!))));
    //    document.Add(carsXml);
    //    document.Save("fuel.xml");
    //}

    //private static void QueryXml()
    //{
    //    var document = XDocument.Load("fuel.xml");
    //    var names=document
    //        .Element("Cars")?
    //        .Elements("Car")
    //        .Where(x=>x.Attribute("Manufacturer")?.Value=="BMW")
    //        .Select(x=>x.Attribute("Name")).ToList();
    //    foreach(var name in names!) 
    //    {
    //    Console.WriteLine(name);
    //    }
    //}

}



