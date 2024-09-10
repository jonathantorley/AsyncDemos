using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestProject1;
public class YieldTests(ITestOutputHelper outputHelper)
{
    async Task Dummy()
    {
        outputHelper.WriteLine("Dummy starting");
        await Task.Delay(1000);
        outputHelper.WriteLine("Dummy ending");
    }

    [Fact]
    public async Task WithYield()
    {
        async Task DoThing()
        {
            outputHelper.WriteLine("Starting DoThing()");
            await Task.Yield();
            Thread.Sleep(1000);
            outputHelper.WriteLine("DoThing invoking Dummy");
            await Dummy();
            outputHelper.WriteLine("DoThing Complete");
        }

        var t = DoThing();
        outputHelper.WriteLine("I have a running thing");
        await t;
        outputHelper.WriteLine("I'm done");
    }

    [Fact]
    public async Task WithoutYield()
    {
        async Task DoThing()
        {
            outputHelper.WriteLine("Starting DoThing()");
            Thread.Sleep(1000);
            outputHelper.WriteLine("DoThing invoking Dummy");
            await Dummy();
            outputHelper.WriteLine("DoThing Complete");
        }

        var t = DoThing();
        outputHelper.WriteLine("I have a running thing");
        await t;
        outputHelper.WriteLine("I'm done");
    }

    [Fact]
    public async Task Synchronous()
    {
        Task DoThing()
        {
            outputHelper.WriteLine("Starting DoThing()");
            Thread.Sleep(1000);
            outputHelper.WriteLine("DoThing invoking Dummy");
            var dummy = Dummy();
            outputHelper.WriteLine("DoThing Complete");
            return dummy;
        }

        var t = DoThing();
        outputHelper.WriteLine("I have a running thing");
        await t;
        outputHelper.WriteLine("I'm done");
    }
}
