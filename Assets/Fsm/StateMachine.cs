using System;

namespace Fsm
{
    public class StateMachine<T>
    {
        enum UpdateType
        {
            Regular,
            Fixed
        }
        class InstantStateChange : Exception
        {
            public State<T> State;
            public InstantStateChange(State<T> state)
            {
                this.State = state;
            }
        }
        public State<T> CurrentState { get; private set; }
        public State<T> PreviousState { get; private set; }
        public State<T> NextState { get; private set; }
        public StateMachine()
        {
            PreviousState = null;
            NextState = null;
            CurrentState = null;
        }
        void UpdateAndHandleStateChange(UpdateType updateType)
        {
            try
            {
                if (updateType == UpdateType.Regular)
                {
                    CurrentState?.Update();
                }
                else if (updateType == UpdateType.Fixed)
                {
                    CurrentState?.FixedUpdate();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("updateType", updateType, "not a valid UpdateType");
                }
                // After the update, check for deferred state change request.
                HandleDeferredStateChange();
            }
            catch (InstantStateChange stateChange)
            {
                // Caught instant state change request. Change the state directly.
                ChangeState(stateChange.State);
            }
        }
        public void Update()
        {
            UpdateAndHandleStateChange(UpdateType.Regular);
        }
        public void FixedUpdate()
        {
            UpdateAndHandleStateChange(UpdateType.Fixed);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shouldOverride">True if this should override any existing deferred state change.</param>
        public void ChangeStateDeferred(State<T> state, bool shouldOverride = false)
        {
            if (NextState != null || shouldOverride)
            {
                NextState = state;
            }
        }
        // This is bad in terms of control flow because it instantly breaks out of any state processing
        // method that it was in and changes the state. I hope I never have to use this.
        public void ChangeStateInstant(State<T> state)
        {
            throw new InstantStateChange(state);
        }
        protected void ChangeState(State<T> nextState)
        {
            if (nextState == null) throw new ArgumentNullException("nextState");

            CurrentState?.Exit();
            PreviousState = CurrentState;
            CurrentState = nextState;
            NextState = null;
            CurrentState.Enter();
        }
        private void HandleDeferredStateChange()
        {
            if (NextState != null)
            {
                ChangeState(NextState);
            }
        }
    }
}