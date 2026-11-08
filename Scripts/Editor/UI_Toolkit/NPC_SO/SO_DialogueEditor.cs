using UnityEditor;

[CustomEditor(typeof(SO_Dialogue))]
public class SO_DialogueEditor : BaseSOEditor
{
    protected override string UxmlPath => "Assets/Project_Data/Scripts/Editor/UI_Toolkit/NPC_SO/SO_DialogueEditor.uxml";
}