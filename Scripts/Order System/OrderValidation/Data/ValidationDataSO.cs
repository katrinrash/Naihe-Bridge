using UnityEngine;

/// <summary>
/// Data storage for order validation requirements, defining the required skill checks and customer preferences for each dish.
/// </summary>

[CreateAssetMenu(fileName = "ValidationData", menuName = "OrdersData/ValidationData")]
public class ValidationDataSO : ScriptableObject
{
    [SerializeField] private StructForDictionary<DishesEnum, int> validationRequirements;

    public DishesEnum GetDish() => validationRequirements.Key;
    public int GetRequiredPoints() => validationRequirements.Value;
}
