using UnityEngine;

/// <summary>
/// Class that holds the game settings.
/// </summary>
public class GameSettings : MonoBehaviour
{
    public static bool enableDebug = false;
    public KeyCode debugEnableKey = KeyCode.D;

    void Update()
    {
        if (Input.GetKeyDown(debugEnableKey))
            enableDebug = !enableDebug;
    }
}
