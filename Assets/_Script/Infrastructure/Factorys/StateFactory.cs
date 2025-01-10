using _Script.Infrastructure.FSM.States;
using VContainer;
using VContainer.Unity;

namespace _Script.Infrastructure.Factorys
{
    public class StateFactory : IStateFactory
    {
        private readonly LifetimeScope _parentScope;

        public StateFactory(LifetimeScope parentScope) => 
            _parentScope = parentScope;

        public IExitableState CreateSystem<TState>() where TState : class, IExitableState => 
            _parentScope.CreateChild(builder =>
                builder.Register<TState>(Lifetime.Transient)).Container.Resolve<TState>();
    }

    public interface IStateFactory
    {
        IExitableState CreateSystem<TState>() where TState : class, IExitableState;
    }
}