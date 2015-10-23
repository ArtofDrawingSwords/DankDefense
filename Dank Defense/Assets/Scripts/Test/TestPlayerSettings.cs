using System;
using UnityEngine;

[Serializable]
public class TestPlayerSettings
{
    public GameObject playerObj;
    public string playerName;
    public TestPlayerController playerController;
    public TestNetworkPlayer n_Player;

    public void Setup()
    {
        playerController = playerObj.GetComponent<TestPlayerController>();
        n_Player = playerObj.GetComponent<TestNetworkPlayer>();
        n_Player.playerName = playerName;
    }
}
