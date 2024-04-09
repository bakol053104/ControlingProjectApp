using ControlingProjectApp.Entities;


namespace ControlingProjectApp.Services.InquiryData;

public interface IInquiryDataProviderForEmployees
{
    public List<Employee>? EmployeesWithJobPosition(int jobposition);

    public List<Employee> EmployeesWithDateEmploymentGreaterThan(DateTime dateEmployment);

    public List<Employee> EmployeesWithDateEmploymentLessThan(DateTime dateEmployment);

    public List<Employee> OrderByLastNameAndFirstName();

    public List<Employee> OrderByLastNameAndFirstNameDescending();

}
