using UnityEditor;

[CustomEditor(typeof(NPC_Animation_Data_SO))]
public class NPCAnimationDataEditor : BaseSOEditor
{
    protected override string UxmlPath => "Assets/Project_Data/Scripts/Editor/UI_Toolkit/NPC_SO/NPCAnimationDataEditor.uxml";
}
