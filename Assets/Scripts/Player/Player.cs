using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Player : MonoBehaviour
    {
        // Start is called before the first frame update
        [HideInInspector] public Animator animator;
        [HideInInspector] public CharacterController controller;
        [HideInInspector] public Vector2 LookAngles = Vector2.zero;
        StateMachine sm;
        public Camera cam;
        void Start()
        {
            cam.transform.parent = transform;
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
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

        private void LateUpdate()
        {

            var lookAngles = Quaternion.Euler(LookAngles);
            // Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 0.5f), lookAngles);
            cam.transform.localPosition = lookAngles * Vector3.back * 2;
            cam.transform.rotation = Quaternion.Euler(LookAngles);
        }
        public void Damage()
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}