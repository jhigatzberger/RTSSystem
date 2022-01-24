using UnityEngine;
using UnityEditor;
using JHiga.RTSEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;

[CustomEditor(typeof(EntityGroup))]
[CanEditMultipleObjects]
public class CustomGroupEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();
        inspector.Add(new NameField(target));
        if (!AssetDatabase.GetAssetPath(target).Equals(""))
        {
            Button addChildGroupButton = new Button(() =>
                TypePickerWindow.Show<EntityGroup>((g) =>
                {
                    ((EntityGroup)target).children.Add(g);
                    if (EntityTreeViewWindow.Instance != null)
                        EntityTreeViewWindow.Instance.RenderTree();
                }, target));
            addChildGroupButton.text = "Add Subgroup";
            inspector.Add(addChildGroupButton);
        }
        return inspector;        
    }
}