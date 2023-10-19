namespace Enemy
{
    public class AttackState : BaseState
    {
        public AttackState(StateMachine stateMachine) : base(stateMachine) { }
        public sealed override void Enter()
        {
            var target = Self.target;
            base.Enter();
            Self.agent.isStopped = false;
            Self.agent.destination = target.transform.position;
        }

        public sealed override void LogicUpdate()
        {
            base.LogicUpdate();
            var target = Self.target;
            if (!Self.WithinActivationRange())
            {
                stateMachine.ChangeStateDeferred(stateMachine.IdleState);
                return;
            }
            if (Self.WithinRange(1))
            {
                Self.animator.SetTrigger("attack");
                Self.agent.isStopped = true;
                return;
            }
            Self.agent.destination = target.transform.position;
        }
    }
}
