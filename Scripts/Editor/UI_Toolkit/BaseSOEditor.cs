using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

/// <summary>
/// Base class for custom ScriptableObject inspectors using UI Toolkit.
/// </summary>

public abstract class BaseSOEditor : Editor
{
    private const string CONST_CommonStylePath = "Assets/Project_Data/Scripts/Editor/UI_Toolkit/CommonEditorStyle.uss";
    protected abstract string UxmlPath { get; }

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        // Load the custom inspector layout
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);

        visualTree.CloneTree(root);

        // Apply the common editor styles
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(CONST_CommonStylePath);

        root.styleSheets.Add(styleSheet);
        root.Bind(serializedObject);

        return root;
    }
}