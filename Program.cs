using ControlingProjectApp;
using ControlingProjectApp.Components.ProviderData;
using ControlingProjectApp.Data;
using ControlingProjectApp.Data.Entities;
using ControlingProjectApp.Data.Repositories;
using ControlingProjectApp.Services;
using ControlingProjectApp.Services.InquiryData;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IApp, App>();
services.AddSingleton<IDataProviderSql, DataProviderSql>();
services.AddSingleton<IUserMenu, UserMenu>();
services.AddSingleton<IUserSubMenu, UserSubMenu>();
services.AddSingleton<IEmployeeData, EmployeeData>();
services.AddSingleton<IProjectData, ProjectData>();
services.AddSingleton<IInquiryProviderForEmployees, InquiryProviderForEmployees>();
services.AddSingleton<IInquiryProviderForProjects, InquiryProviderForProjects>();
services.AddSingleton<IInquiryDataProviderForEmployees, InquiryDataProviderForEmployees>();
services.AddSingleton<IInquiryDataProviderForProjects, InquiryDataProviderFoProjects>();

services.AddDbContext<ControlingProjectAppDbContext>();
services.AddSingleton<IEventSet, EventSet>();
services.AddSingleton<IRepository<Employee>, SqlRepository<Employee>>();
services.AddSingleton<IRepository<Project>, SqlRepository<Project>>();

var serviceProviders = services.BuildServiceProvider();
var app = serviceProviders.GetService<IApp>()!;
app.Run();

