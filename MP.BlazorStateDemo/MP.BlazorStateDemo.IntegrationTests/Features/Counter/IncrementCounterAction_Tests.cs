using FluentAssertions;
using MP.BlazorStateDemo.Core.Application.Features.Counter;
using MP.BlazorStateDemo.IntegrationTests.Infrastructure;
using System.Threading.Tasks;
using static MP.BlazorStateDemo.Core.Application.Features.Counter.CounterState;

namespace MP.BlazorStateDemo.IntegrationTests;
public class IncrementCounterAction_Should : BaseTest
{
    private CounterState CounterState => Store.GetState<CounterState>();

    public IncrementCounterAction_Should(ClientHost aWebAssemblyHost)
        : base(aWebAssemblyHost) { }

    public async Task Decrement_Count_Given_NegativeAmount()
    {
        CounterState.Initialize();

        var incrementCounterRequest = new IncrementCounterAction();

        await Send(incrementCounterRequest);

        CounterState.Count
            .Should()
            .Be(1);
    }

    public async Task Increment_Count()
    {
        CounterState.Initialize();

        var incrementCounterAction = new DecrementCounterAction();

        await Send(incrementCounterAction);

        CounterState.Count
            .Should()
            .Be(-1);
    }
}
