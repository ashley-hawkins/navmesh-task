using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Player
{
    public class NormalState : BaseState
    {
        public NormalState(StateMachine stateMachine) : base(stateMachine) { }
        Vector2 vel = Vector2.zero;
        public override void ProcessInput()
        {
            base.ProcessInput();
            vel = Vector2.zero;
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");
            Self.LookAngles.y = (Self.LookAngles.y + mouseX * 5) % 360;
            Self.LookAngles.x = (Self.LookAngles.x + mouseY * 5) % 360;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                vel += Vector2.up;
            }
            vel = Quaternion.Euler(0, 0, -Self.LookAngles.y) * vel.normalized * 4;
            //Debug.Log(Self.LookAngles);
            //Debug.Log(vel);
            Self.controller.SimpleMove(new Vector3(vel.x, 0, vel.y));
        }
        public sealed override void LogicUpdate()
        {
        }
    }
}
