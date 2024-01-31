using Hanssens.Net;
using HR_Management.MVC.Contracts;
namespace HR_Management.MVC.Services;

public class LocalStorageService : ILocalStorageService
{
    private readonly LocalStorage _localStorage;

    public LocalStorageService()
    {
        LocalStorageConfiguration config = new() { AutoLoad = true, AutoSave = true, Filename = "HR.LEAVEMGMT" };
        _localStorage = new LocalStorage(config);
    }

    public void ClearStorage(List<string> keys)
    {
        foreach (string key in keys)
            _localStorage.Remove(key);
    }

    public bool Exists(string key)
    {
        return _localStorage.Exists(key);
    }

    public T GetStorageValue<T>(string key)
    {
        return _localStorage.Get<T>(key);
    }

    public void SetStorageValue<T>(string key, T value)
    {
        _localStorage.Store(key, value);
        _localStorage.Persist();
    }
}