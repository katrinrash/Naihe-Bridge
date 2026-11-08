using UnityEditor;

[CustomEditor(typeof(SpawningData_SO))]
public class SpawningDataEditor : BaseSOEditor
{
    protected override string UxmlPath => "Assets/Project_Data/Scripts/Editor/UI_Toolkit/NPC_SO/SpawningDataEditor.uxml";
}