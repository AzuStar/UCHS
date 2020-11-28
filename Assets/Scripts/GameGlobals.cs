using UnityEngine;

public class GameGlobals : MonoBehaviour
{
    public static GameGlobals _Self;

    public PlayerController PlayerController;

    public void Awake()
    {
        _Self = this;
    }
}