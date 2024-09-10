namespace TestProject1;

public class ThrowingTests
{
    [Fact]
    public async Task TestThrowingBeforeYield()
    {
        async Task func()
        {
            throw new Exception();
#pragma warning disable CS0162 // Unreachable code detected
            await Task.Yield();
#pragma warning restore CS0162 // Unreachable code detected
        }

        var task = func();
        await Assert.ThrowsAsync<Exception>(async () => { await task; });
    }

    [Fact]
    public async Task TestThrowingAfterYield()
    {
        async Task func()
        {
            await Task.Yield();
            throw new Exception();
        }

        var task = func();
        await Assert.ThrowsAsync<Exception>(async () => { await task; });
    }

    [Fact]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task TestThrowingWithoutAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        Task func()
        {
            throw new Exception();
        }

        Task task;
        Assert.Throws<Exception>(() => { task = func(); });
    }
}
