namespace Oktopost.Storage;

public interface IStore
{
    string? Get(string key);
    void Put(string key, string item);
}
