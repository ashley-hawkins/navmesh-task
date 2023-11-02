using UnityEngine;
using UnityEngine.Experimental.AI;

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

            if (Self.shouldHandleAttack)
            {
                var BoxOrigin = Self.transform.position + (Self.transform.rotation * Vector3.forward) + new Vector3(0, 1, 0);
                Debug.DrawLine(Self.transform.position, BoxOrigin, Color.red, 1.0f);
                var overlaps = Physics.OverlapBox(BoxOrigin, Vector3.one * Self.hurtboxSize);
                foreach (var collider in overlaps)
                {
                    Debug.Log(collider);
                    var Player = collider.GetComponent<Player.Player>();
                    if (Player)
                    {
                        Player.Damage();
                        break;
                    }
                }
                Self.shouldHandleAttack = false;
            }

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
