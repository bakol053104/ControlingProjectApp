using ControlingProjectApp.Components.ProviderData;
using ControlingProjectApp.Services;

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
        _dataProviderSql.AddToDataBaseEmployees();
        _dataProviderSql.AddToDataBaseProjects();
        _userMenu.MainUserMenuSelect();
    }
}



