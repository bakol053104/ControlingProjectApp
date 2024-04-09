using ControlingProjectApp.Data.Repositories;
using ControlingProjectApp.Entities;

namespace ControlingProjectApp.Services.InquiryData;

public class InquiryDataProviderFoProjects: IInquiryDataProviderForProjects
{
        private readonly IRepository<Project> _projectsRepository;

        public InquiryDataProviderFoProjects(IRepository<Project> projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

    public List<Project> OrderByName()
    {
        var projects = _projectsRepository.GetAll();
        var list = projects.OrderBy(x => x.Name)
           .ThenBy(x => x.Name).ToList();
        return list;
    }

    public List<Project> OrderByNameDescending()
    {
        var projects = _projectsRepository.GetAll();
        var list = projects.OrderByDescending(x => x.Name)
           .ThenByDescending(x => x.Name).ToList();
        return list;
    }

    public List<Project> OrderByMaxValue()
    {
        var projects = _projectsRepository.GetAll();
        var list = projects.OrderByDescending(x => x.Budget)
            .ToList();
        return list;
    }

    public List<Project> OrderByMinValue()
    {
        var projects = _projectsRepository.GetAll();
        var list = projects.OrderBy(x => x.Budget)
            .ToList();
        return list;
    }

    public decimal SumValue()
    {
        var projects = _projectsRepository.GetAll();
        return projects.Select(x => x.Budget).ToArray().Sum();
    }
}