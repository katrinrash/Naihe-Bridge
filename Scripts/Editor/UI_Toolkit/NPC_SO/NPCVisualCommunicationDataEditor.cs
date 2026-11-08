using UnityEditor;

[CustomEditor(typeof(NPC_VisualCommunicationData_SO))]
public class NPCVisualCommunicationDataEditor : BaseSOEditor
{
    protected override string UxmlPath => "Assets/Project_Data/Scripts/Editor/UI_Toolkit/NPC_SO/NPCVisualCommunicationDataEditor.uxml";
}
