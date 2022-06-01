using BlazorState;
using MediatR;

namespace MP.BlazorStateDemo.Core.Application.Features.Counter;

public partial class CounterState
{
    public class IncrementCountHandler : ActionHandler<IncrementCounterAction>
    {
        public IncrementCountHandler(IStore aStore) : base(aStore)
        {
        }

        private CounterState CounterState => Store.GetState<CounterState>();

        public override Task<Unit> Handle(
            IncrementCounterAction incrementCountAction,
            CancellationToken aCancellationToken)
        {
            CounterState.Count += incrementCountAction.Amount;
            return Unit.Task;
        }
    }
}