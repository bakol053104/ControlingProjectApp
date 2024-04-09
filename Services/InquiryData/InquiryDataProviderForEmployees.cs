using ControlingProjectApp.Data.Repositories;
using ControlingProjectApp.Entities;

namespace ControlingProjectApp.Services.InquiryData;

public class InquiryDataProviderForEmployees : UserSubMenuBase, IInquiryDataProviderForEmployees
{
    private readonly IRepository<Employee> _employeesRepository;

    public InquiryDataProviderForEmployees(IRepository<Employee> employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public List<Employee>? EmployeesWithJobPosition(int jobposition)
    {
        var employees = _employeesRepository.GetAll();
        var list = employees
               .Where(x => x.JobPositon == (JobPosition)jobposition)
               .ToList();
        return list;

    }

    public List<Employee> EmployeesWithDateEmploymentGreaterThan(DateTime dateEmployment)
    {
        var employees = _employeesRepository.GetAll();
        var list = employees
               .Where(x => x.EmploymentDate >= dateEmployment)
               .OrderBy(x => x.EmploymentDate)
               .ToList();
        return list;
    }

    public List<Employee> EmployeesWithDateEmploymentLessThan(DateTime dateEmployment)
    {
        var employees = _employeesRepository.GetAll();
        var list = employees
               .Where(x => x.EmploymentDate <= dateEmployment)
               .OrderByDescending(x => x.EmploymentDate)
               .ToList();
        return list;
    }
    public List<Employee> OrderByLastNameAndFirstName()
    {
        var employees = _employeesRepository.GetAll();
        var list=  employees.OrderBy(x => x.LastName)
           .ThenBy(x => x.FirstName).ToList();
        return list;
    }

    public List<Employee> OrderByLastNameAndFirstNameDescending()
    {
        var employees = _employeesRepository.GetAll();
        var list = employees.OrderByDescending(x => x.LastName)
           .ThenByDescending(x => x.FirstName).ToList();
        return list;
    }

}



