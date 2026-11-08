using UnityEditor;

[CustomEditor(typeof(NPC_Data_SO))]
public class NPCDataSOEditor : BaseSOEditor
{
    protected override string UxmlPath => "Assets/Project_Data/Scripts/Editor/UI_Toolkit/NPC_SO/Base/NPCDataSOEditor.uxml";
}
