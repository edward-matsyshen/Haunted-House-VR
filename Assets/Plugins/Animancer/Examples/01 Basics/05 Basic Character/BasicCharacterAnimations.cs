// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2023 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using UnityEngine;
using UnityEngine.XR; // Include the XR namespace for Oculus controls.

namespace Animancer.Examples.Basics
{
    [AddComponentMenu(Strings.ExamplesMenuPrefix + "Basics - Basic Character Animations")]
    [HelpURL(Strings.DocsURLs.ExampleAPIDocumentation + nameof(Basics) + "/" + nameof(BasicCharacterAnimations))]
    public sealed class BasicCharacterAnimations : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent _Animancer;
        [SerializeField] private ClipTransition _Idle;
        [SerializeField] private ClipTransition _Move;
        [SerializeField] private ClipTransition _Action;

        private enum State
        {
            NotActing,
            Acting,
        }

        private State _CurrentState;

        private void Awake()
        {
            _Action.Events.OnEnd = OnActionEnd;
        }

        private void OnActionEnd()
        {
            _CurrentState = State.NotActing;
            UpdateMovement();
        }

        private void Update()
        {
            switch (_CurrentState)
            {
                case State.NotActing:
                    UpdateMovement();
                    UpdateAction();
                    break;
                case State.Acting:
                    UpdateAction();
                    break;
            }
        }

        private void UpdateMovement()
        {
            var inputDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axis); // Using the thumbstick on the left controller.

            if (axis.y > 0.1) // Assuming forward movement with a threshold to prevent drift.
            {
                _Animancer.Play(_Move);
            }
            else
            {
                _Animancer.Play(_Idle);
            }
        }

        private void UpdateAction()
        {
            var inputDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            if (inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed) // Use the trigger button to initiate actions.
            {
                _CurrentState = State.Acting;
                _Animancer.Play(_Action);
            }
        }
    }
}