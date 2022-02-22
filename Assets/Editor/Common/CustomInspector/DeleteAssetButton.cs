using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DeleteAssetButton : VisualElement
{
    public DeleteAssetButton(Object target)
    {
        Button button = new Button(()=> Delete(target));
        button.text = "Delete";
        Add(button);
    }

    private void Delete(Object target)
    {
        Object.DestroyImmediate(target, true);
        AssetDatabase.SaveAssets();
    }
}
