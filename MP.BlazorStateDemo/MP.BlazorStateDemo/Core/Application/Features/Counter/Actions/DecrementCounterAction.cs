using BlazorState;

namespace MP.BlazorStateDemo.Core.Application.Features.Counter;

public partial class CounterState
{
    public class DecrementCounterAction : IAction
    {
        public int Amount => 1;
    }
}