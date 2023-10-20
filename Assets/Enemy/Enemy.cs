using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float ActivationDistance = 5;
        // Start is called before the first frame update
        StateMachine sm;
        [HideInInspector] public Animator animator;
        [HideInInspector] public NavMeshAgent agent;
        public GameObject target;

        public bool WithinRange(float range)
        {
            // Multiplying by (1, 0, 1) removes Y component.
            return Vector3.Scale((target.transform.position - transform.position), new Vector3(1, 0, 1)).sqrMagnitude <= (range * range);
        }
        public bool WithinActivationRange()
        {
            return WithinRange(ActivationDistance);
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            sm = new StateMachine(this);
            agent = GetComponent<NavMeshAgent>();
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