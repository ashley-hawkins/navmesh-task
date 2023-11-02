namespace Player
{
    [System.Serializable]
    public class StateMachine : Fsm.StateMachine<StateMachine>
    {
        public Player Self { get; private set; }
        public StateMachine(Player self) : base()
        {
            Self = self;

            NormalState = new NormalState(this);
            ChangeStateDeferred(NormalState);
        }

        public NormalState NormalState;
    }
}
