using UnityEditor;
using UnityEngine;

public class StateMachineData : ScriptableObject
{
    private static StateMachineData _instance;
    public static StateMachineData Instance
    {
        get 
        {
            if(_instance == null)
            {
                StateMachineData asset = Resources.Load<StateMachineData>("Assets/Resources/RTS/StateMachineData");
                if (asset == null)
                {
                    asset = CreateInstance<StateMachineData>();
                    AssetDatabase.CreateAsset(asset, "Assets/Resources/RTS/StateMachineData");
                    AssetDatabase.SaveAssets();
                }
                _instance = asset;
            }
            return _instance;
        }
    }
}
