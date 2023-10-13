﻿namespace Enemy
{
    public class IdleState : BaseState
    {
        public IdleState(StateMachine stateMachine) : base(stateMachine) { }
        public sealed override void LogicUpdate()
        {
            base.LogicUpdate();
            var target = Self.target;
            if (target.transform.position - Self.transform.position)
            {

            }
        }
    }
}
