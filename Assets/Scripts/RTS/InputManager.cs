using UnityEngine;
using UnityEngine.Events;

namespace RTS
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PointerTrigger[] triggers;

        public LayerMask groundLayerMask;

        void Update()
        {
            foreach (PointerTrigger trigger in triggers)
                trigger.Update();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, groundLayerMask))
            {
                Debug.DrawLine(ray.origin, hit.point);
                Context.worldPointerPosition = hit.point;
            }
            else
                Context.worldPointerPosition = null;
        }
    }

    [System.Serializable]
    public class PointerTrigger
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