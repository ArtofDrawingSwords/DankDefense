using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

public class TestGod : NetworkBehaviour {

    static public TestGod Kamisama;
    //public List<GameObject> Players = new List<GameObject>();
    public List<TestPlayerSettings> players = new List<TestPlayerSettings>();
    void Awake()
    {
        Kamisama = this;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //God Functions
     public void AddPlayer(GameObject player, string playerName)
    {
        TestPlayerSettings temp = new TestPlayerSettings();
        temp.playerObj = player;
        temp.playerName = playerName;
        temp.Setup();
        players.Add(temp);
    }
}
