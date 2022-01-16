using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;

class EntityTreeViewWindow : EditorWindow
{
    // SerializeField is used to ensure the view state is written to the window 
    // layout file. This means that the state survives restarting Unity as long as the window
    // is not closed. If the attribute is omitted then the state is still serialized/deserialized.
    [SerializeField] TreeViewState m_TreeViewState;

    //The TreeView is not serializable, so it should be reconstructed from the tree data.
    EntityTreeView m_SimpleTreeView;

    void OnEnable()
    {
        // Check whether there is already a serialized view state (state 
        // that survived assembly reloading)
        if (m_TreeViewState == null)
            m_TreeViewState = new TreeViewState();

        m_SimpleTreeView = new EntityTreeView(m_TreeViewState);
    }

    void OnGUI()
    {
        m_SimpleTreeView.OnGUI(new Rect(0, 0, position.width, position.height));
    }

    // Add menu named "My Window" to the Window menu
    [MenuItem("RTS/Entity Tree")]
    static void ShowWindow()
    {
        // Get existing open window or if none, make a new one:
        var window = GetWindow<EntityTreeViewWindow>();
        window.titleContent = new GUIContent("Entity Tree");
        window.Show();
    }
}