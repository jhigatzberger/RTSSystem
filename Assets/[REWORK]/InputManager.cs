using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace JHiga.RTSEngine.InputHandling
{
    /// <summary>
    /// TODO: USE NEW INPUT MANAGER
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static Vector3? worldPointerPosition;
        [SerializeField] private InputTrigger[] triggers;

        
        private LayerMask groundLayerMask;
        private void Start()
        {
            groundLayerMask = RTSWorldData.Instance.groundLayerMask;
        }

        void Update()
        {
            foreach (InputTrigger trigger in triggers)
                trigger.Update();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, groundLayerMask))
            {
                Debug.DrawLine(ray.origin, hit.point);
                worldPointerPosition = hit.point;
            }
            else
                worldPointerPosition = null;
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
        public bool triggerOverUI;
        
        public void Update()
        {
            
            if (!_active && Input.GetAxisRaw(inputAxis) == triggerValue && (triggerOverUI || !triggerOverUI && !EventSystem.current.IsPointerOverGameObject()))
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