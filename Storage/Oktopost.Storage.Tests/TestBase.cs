using System.Diagnostics;

namespace Oktopost.Storage.Tests;

public abstract class TestBase<TStore, TStoreFactory>
    where TStore : IStore
    where TStoreFactory : IStoreFactory<TStore>, new()
{
    private readonly TStoreFactory _storeFactory = new TStoreFactory();

    [Fact]
    public void Test_Get_ItemNotFound_ReturnNull()
    {
        var store = _storeFactory.Create(10);
        Assert.Null(store.Get("a"));
    }

    [Fact]
    public void Test_Put_NoStorageSpaceLeft_ItemUntouchedForTheMostRecentIsRemoved()
    {
        var store = _storeFactory.Create(2);
        store.Put("a", "val_1");
        store.Put("b", "val_2");

        store.Get("a");
        store.Put("c", "val_3");

        Assert.Equal("val_1", store.Get("a"));
        Assert.Null(store.Get("b"));
        Assert.Equal("val_3", store.Get("c"));
    }

    [Fact]
    public void Test_Put_PriorityOfAnItemReset()
    {
        var store = _storeFactory.Create(2);
        store.Put("a", "val_1");
        store.Put("b", "val_2");

        store.Put("a", "val_1_2");
        store.Put("c", "val_3");

        Assert.Equal("val_1_2", store.Get("a"));
        Assert.Null(store.Get("b"));
        Assert.Equal("val_3", store.Get("c"));
    }

    [Fact]
    public void Test_SingleBucket()
    {
        var store = _storeFactory.Create(1);

        store.Put("a", "val_1");
        Assert.Equal("val_1", store.Get("a"));

        store.Put("b", "val_2");
        Assert.Null(store.Get("a"));
        Assert.Equal("val_2", store.Get("b"));
    }

    [Fact]
    public void Test_ThreeBucket()
    {
        var store = _storeFactory.Create(3);

        store.Put("a", "val_1");
        store.Put("b", "val_2");
        store.Put("c", "val_3");

        Assert.Equal("val_1", store.Get("a"));
        Assert.Equal("val_2", store.Get("b"));
        Assert.Equal("val_3", store.Get("c"));

        store.Put("d", "val_4");
        Assert.Null(store.Get("a"));
        Assert.Equal("val_2", store.Get("b"));
        Assert.Equal("val_3", store.Get("c"));
        Assert.Equal("val_4", store.Get("d"));
    }

    [Fact]
    public void Test_ThreeBucket_AccessMiddleElement()
    {
        var store = _storeFactory.Create(3);

        store.Put("a", "val_1");
        store.Put("b", "val_2");
        store.Put("c", "val_3");

        // forcing removal from the middle of the linked list
        Assert.Equal("val_2", store.Get("b"));
        Assert.Equal("val_3", store.Get("c"));
        Assert.Equal("val_1", store.Get("a"));
    }

    [Fact]
    public void Test_MaxItemsValidation()
    {
        Assert.Throws<ArgumentException>(() => _storeFactory.Create(0));
        Assert.Throws<ArgumentException>(() => _storeFactory.Create(-1));
    }
}