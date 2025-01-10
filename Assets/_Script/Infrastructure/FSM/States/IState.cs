namespace _Script.Infrastructure.FSM.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    
    public interface IExitableState
    {
        void Exit();
    }
}