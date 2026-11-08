using UnityEditor;

[CustomEditor(typeof(SG_Data_SO))]
public class SGDataSOEditor : BaseSOEditor
{
    protected override string UxmlPath => "Assets/Project_Data/Scripts/Editor/UI_Toolkit/NPC_SO/Base/SGDataSOEditor.uxml";
}
