using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Utility class for converting mouse input into world-space rays and positions.
/// 
/// Use cases:
/// - Detecting clicks on game objects
/// - Converting mouse position into world coordinates to move objects 
/// </summary>

public static class RaycastUtility
{
    private static readonly float Z = 12f;
    
    public static Ray GetRay()
    {
        var mousePosition = Mouse.current.position.ReadValue();

        return Camera.main.ScreenPointToRay(mousePosition);
    }
    
    public static Vector3 GetPoint()
    {
        var mousePosition = Mouse.current.position.ReadValue();

        mousePosition = Camera.main.ScreenToWorldPoint(new Vector2(mousePosition.x, mousePosition.y));

        return new Vector3(mousePosition.x, mousePosition.y, Z);
    }
}
