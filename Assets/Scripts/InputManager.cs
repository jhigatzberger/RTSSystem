using UnityEngine;
using UnityEngine.Events;

namespace RTS
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputTrigger[] triggers;

        void Update()
        {
            foreach (InputTrigger trigger in triggers)
                trigger.Update();
        }
    }

    [System.Serializable]
    public class InputTrigger
    {
        public string inputAxis;
        bool _active;
        public float triggerValue;
        public UnityEvent onInput;
        public UnityEvent onInputStop;
        public void Update()
        {
            if (!_active && Input.GetAxisRaw(inputAxis) == triggerValue)
            {
                onInput.Invoke();
                _active = true;
            }

            if (Input.GetAxisRaw(inputAxis) != triggerValue && _active)
            {
                onInputStop.Invoke();
                _active = false;
            }
        }
    }
}