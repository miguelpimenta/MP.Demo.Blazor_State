using BlazorState;
using FluentAssertions;
using MediatR;
using MP.BlazorStateDemo.Core.Application.Features.Counter;
using MP.BlazorStateDemo.IntegrationTestsAlt.Extensions;
using Xunit.Extensions.Ordering;
using static MP.BlazorStateDemo.Core.Application.Features.Counter.CounterState;

namespace MP.BlazorStateDemo.IntegrationTestsAlt.Core.Application.Features.Counter;

public class CounterStateTests
{
    private readonly ISender Sender;
    protected readonly IStore Store;

    public CounterStateTests(ISender sender, IStore store) =>
        (Sender, Store) = (sender, store);

    private CounterState CounterState => Store.GetState<CounterState>();


    [Fact]
    public async Task IncrementCounterActionExpectedBehavior()
    {
        CounterState.Initialize();

        var incrementCounterRequest = new IncrementCounterAction();

        await Sender.Send(incrementCounterRequest);

        CounterState.Count
            .Should()
            .Be(1);
    }

    [Fact]
    public async Task DecrementCounterActionExpectedBehavior()
    {
        CounterState.Initialize();

        var decrementCounterAction = new DecrementCounterAction();

        await Sender.Send(decrementCounterAction);

        CounterState.Count
            .Should()
            .Be(-1);
    }


    [Theory(DisplayName = "Increment Count Multiple Times"), Order(5)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(30)]
    [InlineData(60)]
    public async Task IncrementCountMultipleTimesExpectedBehavior(int clicks)
    {
        CounterState.Initialize();

        var incrementCounterRequest = new IncrementCounterAction();

        1.UpTo(clicks, async (_) =>
            await Sender.Send(incrementCounterRequest));

        CounterState.Count
            .Should()
            .Be(clicks);
    }

    [Theory(DisplayName = "Decrement Count Multiple Times"), Order(5)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(30)]
    [InlineData(60)]
    public async Task DecrementCountMultipleTimesExpectedBehavior(int clicks)
    {
        CounterState.Initialize();

        var decrementCounterRequest = new DecrementCounterAction();

        1.UpTo(clicks, async (_) =>
            await Sender.Send(decrementCounterRequest));

        CounterState.Count
            .Should()
            .Be(-clicks);
    }

}