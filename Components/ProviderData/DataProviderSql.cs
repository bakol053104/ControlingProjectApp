using ControlingProjectApp.Data;

namespace ControlingProjectApp.Components.ProviderData;

public class DataProviderSql : DataProvider, IDataProviderSql
{
    private readonly ControlingProjectAppDbContext _dbContext;

    public DataProviderSql(ControlingProjectAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToDataBaseEmployees()
    {
        if (_dbContext.Database.CanConnect() && !_dbContext.Employees.Any())
        {
            var employees = GenerateEmployees();
            _dbContext.Employees.AddRange(employees);
            _dbContext.SaveChanges();
        }
    }

    public void AddToDataBaseProjects()
    {
        if (_dbContext.Database.CanConnect() && !_dbContext.Projects.Any())
        {
            var projects = GenerateProjects();
            _dbContext.Projects.AddRange(projects);
            _dbContext.SaveChanges();
        }
    }

}


