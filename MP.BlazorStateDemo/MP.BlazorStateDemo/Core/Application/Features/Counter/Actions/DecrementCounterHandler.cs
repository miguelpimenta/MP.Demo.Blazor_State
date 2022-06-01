using BlazorState;
using MediatR;

namespace MP.BlazorStateDemo.Core.Application.Features.Counter;

public partial class CounterState
{
    public class DecrementCountHandler : ActionHandler<DecrementCounterAction>
    {
        public DecrementCountHandler(IStore aStore) : base(aStore)
        {
        }

        private CounterState CounterState => Store.GetState<CounterState>();

        public override Task<Unit> Handle(
            DecrementCounterAction decrementCountAction,
            CancellationToken aCancellationToken)
        {
            CounterState.Count -= decrementCountAction.Amount;
            return Unit.Task;
        }
    }
}