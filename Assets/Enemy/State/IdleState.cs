namespace Enemy
{
    public class IdleState : BaseState
    {
        public IdleState(StateMachine stateMachine) : base(stateMachine) { }
        public sealed override void Enter()
        {
            base.Enter();
            Self.agent.isStopped = true;
        }
        public sealed override void LogicUpdate()
        {
            base.LogicUpdate();
            var target = Self.target;
            if (Self.WithinActivationRange())
            {
                stateMachine.ChangeStateDeferred(stateMachine.AttackState);
            }
        }
    }
}
