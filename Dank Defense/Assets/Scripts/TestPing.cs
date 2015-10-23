using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TestPing : NetworkBehaviour {

    private NetworkClient nClient;
    private float latency;

	// Use this for initialization
	void Start () {
        nClient = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().client;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnGUI()
    {
        if(isLocalPlayer)
        {
            GUI.Label(new Rect(10, Screen.height - 50, 250, 50), nClient.GetRTT().ToString());
        }
    }

    void showLatency()
    {

    }
}
