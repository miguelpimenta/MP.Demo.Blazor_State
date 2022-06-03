using FluentAssertions;
using MP.BlazorStateDemo.Core.Application.Features.Counter;
using MP.BlazorStateDemo.Tests.Infrastructure;
using static MP.BlazorStateDemo.Core.Application.Features.Counter.CounterState;

namespace MP.BlazorStateDemo.Tests.Core.Application.Features.Counter;

public class CounterStateTests : BaseTest
{
    private CounterState CounterState => Store.GetState<CounterState>();

    public CounterStateTests(ClientHost aWebAssemblyHost)
        : base(aWebAssemblyHost)
    { }


    public async Task IncrementCounterActionExpectedBehavior()
    {
        CounterState.Initialize();

        var incrementCounterRequest = new IncrementCounterAction();

        await Send(incrementCounterRequest);

        CounterState.Count
            .Should()
            .Be(1);
    }


    public async Task DecrementCounterActionExpectedBehavior()
    {
        CounterState.Initialize();

        var incrementCounterAction = new DecrementCounterAction();

        await Send(incrementCounterAction);

        CounterState.Count
            .Should()
            .Be(-1);
    }
}