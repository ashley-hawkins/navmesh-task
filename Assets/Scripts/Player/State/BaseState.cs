namespace Player
{
    public class BaseState : Fsm.State<StateMachine>
    {
        public Player Self { get { return stateMachine.Self; } }
        public BaseState(StateMachine stateMachine) : base(stateMachine) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Self.animator.SetFloat("vel", Self.controller.velocity.magnitude);
        }
    }
}
