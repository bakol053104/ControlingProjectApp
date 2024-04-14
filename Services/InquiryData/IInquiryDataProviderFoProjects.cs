using ControlingProjectApp.Data.Entities;

namespace ControlingProjectApp.Services.InquiryData;

public interface IInquiryDataProviderForProjects
{
    public List<Project> OrderByName();

    public List<Project> OrderByNameDescending();

    public List<Project> OrderByMaxValue();

    public List<Project> OrderByMinValue();

    public decimal SumValue();

}
