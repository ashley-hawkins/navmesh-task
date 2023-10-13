using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        // Start is called before the first frame update
        StateMachine sm;
        public GameObject target;

        void Start()
        {
            sm = new StateMachine(this);
        }

        // Update is called once per frame
        void Update()
        {
            sm.Update();
        }
        void FixedUpdate()
        {
            sm.FixedUpdate();
        }
    }
}