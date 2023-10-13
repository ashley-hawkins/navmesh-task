namespace Fsm
{
    public abstract class State<T>
    {
        protected T stateMachine;
        public State(T p_stateMachine)
        {
            stateMachine = p_stateMachine;
        }
        public void Update()
        {
            ProcessInput();
            LogicUpdate();
        }
        public void FixedUpdate()
        {
            PhysicsUpdate();
        }
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void ProcessInput() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
    }
}