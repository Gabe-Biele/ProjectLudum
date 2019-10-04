using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectLudum
{
    public class PlayerController : MonoBehaviour
    {
        private Animator characterAnimator;
        private enum PlayerAnimationState { Idle, Walking };
        private static int STATE_MACHINE = Animator.StringToHash("StateMachine");
        private static int INPUT_MAGNITUDE = Animator.StringToHash("InputMagnitude");
        private static int INPUT_ANGLE = Animator.StringToHash("InputAngle");
        //private static int INPUT_HORIZONTAL = Animator.StringToHash("Horizontal");
        //private static int INPUT_VERTICAL = Animator.StringToHash("Vertical");

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            UpdateStateMachineTag();
            SetInputMagnitudeAndAngle();
        }
        /// <summary>
        /// Sets an integer parameter on the Animation Controller so that we can tell what "State Machine" we are in. We then can use the "Any" state as a Local Any that only applies to states in that State Machine.
        /// </summary>
        public void UpdateStateMachineTag()
        {
            if (characterAnimator is null) characterAnimator = GetComponent<Animator>();

            if (characterAnimator.GetCurrentAnimatorStateInfo(0).tagHash == Animator.StringToHash("Idle"))
                characterAnimator.SetInteger(STATE_MACHINE, (int)PlayerAnimationState.Idle);

            if (characterAnimator.GetCurrentAnimatorStateInfo(0).tagHash == Animator.StringToHash("Walking"))
                characterAnimator.SetInteger(STATE_MACHINE, (int)PlayerAnimationState.Walking);
        }

        public void SetInputMagnitudeAndAngle()
        {
            if (characterAnimator is null) characterAnimator = GetComponent<Animator>();
            float inputMag = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")).magnitude;
            characterAnimator.SetFloat(INPUT_MAGNITUDE, inputMag);
            //Debug.Log(inputMag);

            float inputAngle = Vector3.Angle(new Vector3(0, 1), new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            if (Input.GetAxis("Horizontal") < 0) inputAngle = 360 - inputAngle;
            characterAnimator.SetFloat(INPUT_ANGLE, inputAngle);
        }
    }
}
