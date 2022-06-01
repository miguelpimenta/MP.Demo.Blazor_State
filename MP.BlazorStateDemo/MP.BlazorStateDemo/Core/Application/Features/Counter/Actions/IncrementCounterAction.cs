using BlazorState;

namespace MP.BlazorStateDemo.Core.Application.Features.Counter;

public partial class CounterState
{
    public class IncrementCounterAction : IAction
    {
        public int Amount => 1;
    }
}