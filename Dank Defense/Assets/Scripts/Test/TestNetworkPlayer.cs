using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

[Serializable]
public class TestNetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    public string playerName;
    private NetworkInstanceId playerID;

    public override void OnStartLocalPlayer()
    {
        GetPlayerID();
    }

    // Use this for initialization
    void Start()
    {
        /*
        if (isLocalPlayer)
        {
            GetComponent<TestPlayerController>().enabled = true;
        }
        */

        TestGod.Kamisama.AddPlayer(gameObject, playerName + playerID);
    }

    [Client]
    void GetPlayerID()
    {
        playerID = GetComponent<NetworkIdentity>().netId;
    }

}