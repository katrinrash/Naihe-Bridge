using UnityEditor;

[CustomEditor(typeof(ValidationDataSO))]
public class ValidationDataEditor : BaseSOEditor
{
    protected override string UxmlPath => "Assets/Project_Data/Scripts/Editor/UI_Toolkit/Validation_SO/ValidationDataEditor.uxml";
}
