using UnityEngine;
using UnityEngine.InputSystem;

namespace CS25
{
    [RequireComponent (typeof(Rigidbody2D))]
    public abstract class CSBehaviour : MonoBehaviour
    {
        [SerializeField] private InputActionReference MousePositionAction, LMBClickAction;

        private static bool IsLMBPressed;
        protected virtual void OnStart()
        {

        }

        protected virtual void OnUpdate()
        {

        }

        /// <summary>
        /// Calls when player clicked on object (not when they hold button)
        /// </summary>
        protected virtual void OnClickEnter()
        {

        }

        protected virtual void OnLose()
        {

        }

        protected virtual void OnPause()
        {

        }

        protected virtual void OnUnpause()
        {

        }

        private void Start()
        {
            GameState.Instance.StateChanged += (state) =>
            {
                switch(state)
                {
                    case State.None:
                        throw new System.ArgumentException("State changed in none state!");

                    case State.Playing:
                        OnUnpause();
                        break;

                    case State.Death:
                        OnLose();
                        break;

                    case State.Pause:
                        OnPause();
                        break;
                }
            };
            OnStart();
        }

        private void Update()
        {

            var mousePosition = MousePositionAction.action.ReadValue<Vector2>();
            var isMouseButtonPressed = LMBClickAction.action.IsPressed();

            if(IsLMBPressed)
            {
                if(isMouseButtonPressed) return;
                IsLMBPressed = false;
            }

            if(IsUnderMouse(mousePosition) && isMouseButtonPressed)
            {
                IsLMBPressed = true;
                OnClickEnter();
            }

            OnUpdate();
        }

        private bool IsUnderMouse(Vector2 mousePosition)
        {
            return ObjectUnderMouseHelper.Instance.IsUnderMouse(mousePosition, gameObject);
        }
    }
}