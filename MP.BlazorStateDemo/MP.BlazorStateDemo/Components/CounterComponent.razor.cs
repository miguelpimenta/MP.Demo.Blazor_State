using BlazorState;
using MP.BlazorStateDemo.Core.Application.Features.Counter;

namespace MP.BlazorStateDemo.Components;

public partial class CounterComponent : BlazorStateComponent
{
    private CounterState CounterState => GetState<CounterState>();

    private int CurrentCount => CounterState.Count;

    private Task IncrementCount()
    {
        return Mediator
            .Send(new CounterState.IncrementCounterAction());
    }

    private Task DecrementCount()
    {
        return Mediator
            .Send(new CounterState.DecrementCounterAction());
    }
}