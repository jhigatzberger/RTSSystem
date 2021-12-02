using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace RTS
{
    public class SelectableObject : MonoBehaviour
    {
        public DefaultSelectableObject properties;
        public Renderer _renderer;
        public bool Visible
        {
            get
            {
                return _renderer.isVisible;
            }
        }

        public GameObject selectionIndicator;

        public int ownPriority;

        private Selection _unselectableForceSelection;
        public Selection UnselectableForceSelection
        {
            get
            {
                return _unselectableForceSelection;
            }
            set
            {
                _unselectableForceSelection = value;
                UpdateIndicator();
            }
        }
        public SelectionEvent onSelectionChange = new SelectionEvent();
        private Selection _selection;
        public Selection JoinedSelection
        {
            get
            {
                return _unselectableForceSelection==null?_selection: _unselectableForceSelection;
            }
            set
            {
                if (_unselectableForceSelection != null)
                    return;
                _selection = value;
                UpdateIndicator();
                onSelectionChange.Invoke(this);                
            }
        }


        private void Awake()
        {
            if (properties != null)
                properties.Init(this);


            if (_renderer == null)
                _renderer = GetComponent<Renderer>();

            _renderer.gameObject.AddComponent<SelectableOnScreenObject>().Init(this);
            UpdateIndicator();

            Debug.Assert(selectionIndicator != null, "Please make sure " + gameObject.name + " has a selection indicator object");
            Debug.Assert(_renderer != null, "Please assign a renderer to define if the object (" + gameObject.name + ") is on screen (performance reasons)");
        }
        public void UpdateIndicator()
        {
            selectionIndicator.SetActive(JoinedSelection != null && JoinedSelection.priority == ownPriority);
        }
    }

    [System.Serializable]
    public class SelectionEvent : UnityEvent<SelectableObject>
    {
    }


    public class SelectableOnScreenObject : MonoBehaviour
    {
        public static HashSet<SelectableObject> current = new HashSet<SelectableObject>();
        private SelectableObject main;
        public void Init(SelectableObject main)
        {
            this.main = main;
        }
        private void OnBecameVisible()
        {
            current.Add(main);
        }
        private void OnBecameInvisible()
        {
            current.Remove(main);
        }
    }

    [CustomEditor(typeof(SelectableObject))]
    public class SelectableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SelectableObject selectableObject = target as SelectableObject;
            selectableObject.properties = (DefaultSelectableObject)EditorGUILayout.ObjectField("Properties", selectableObject.properties, typeof(DefaultSelectableObject), false);
            if (selectableObject.properties == null)
            {
                selectableObject.selectionIndicator = (GameObject)EditorGUILayout.ObjectField("Selection Indicator", selectableObject.selectionIndicator, typeof(GameObject), true);
                selectableObject.ownPriority = EditorGUILayout.IntField("Priority", 0);
            }
            Renderer ownRenderer = selectableObject.GetComponent<Renderer>();
            if (ownRenderer == null)
                selectableObject._renderer = (Renderer)EditorGUILayout.ObjectField("Renderer", selectableObject._renderer, typeof(Renderer), true);
            else
                selectableObject._renderer = ownRenderer;
        }
    }
}