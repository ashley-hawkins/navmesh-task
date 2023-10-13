namespace Enemy
{
    public class StateMachine : Fsm.StateMachine<StateMachine>
    {
        public Enemy Self { get; private set; }
        public StateMachine(Enemy self) : base()
        {
            Self = self;

            IdleState = new IdleState(this);
            AttackState = new AttackState(this);
            ChangeState(IdleState);
        }

        public IdleState IdleState;
        public AttackState AttackState; 
    }
}
