using NoxRaven;
using UnityEngine;
using static NoxFiretail.Scripts.Core.GameCommon;

public class GameGlobals : MonoBehaviour
{
    public static GameGlobals _Self;

    public PlayerController PlayerController;

    public void Awake()
    {
        _Self = this;
        new Timer(NoxUnit.RegenerationTimeout, true, () => { foreach (NoxUnit ue in NoxUnit.Indexer.Values) ue.Regenerate(); }).Start();
    }
}