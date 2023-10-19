namespace Enemy
{
    public class BaseState : Fsm.State<StateMachine>
    {
        public Enemy Self { get { return stateMachine.Self; } }
        public BaseState(StateMachine stateMachine) : base(stateMachine) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Self.animator.SetFloat("vel", Self.agent.velocity.magnitude);
        }
    }
}
