using _Script.Infrastructure.FSM.States;

namespace _Script.Infrastructure.Factories.Fsm
{
    public interface IStateFactory
    {
        IExitableState CreateSystem<TState>() where TState : class, IExitableState;
    }
}