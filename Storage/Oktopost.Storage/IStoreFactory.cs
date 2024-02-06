namespace Oktopost.Storage;

public interface IStoreFactory<out TStore> where TStore : IStore
{
    TStore Create(int maxItems);
}
