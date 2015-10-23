using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TestPlayerController : NetworkBehaviour {

    //public float speed = 1;
    [SyncVar]
    public float rigidBodySpeed = 100;

    [SyncVar]
    public float moveX = 0;
    [SyncVar]
    public float moveY = 0;

    float x;
    float y;
    float oldX;
    float oldY;

    Transform playerTransform;
    Rigidbody2D playerRigidBody;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject); //Don't delete Player on Map Change etc.
        playerTransform = GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if(!isLocalPlayer)
        {
            return;
        }

        x = 0;
        y = 0;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if(x != oldX || y != oldY)
        {
            CmdMove(x, y);
            oldX = x;
            oldY = y;
        }

        //Kick players to lobby. Admin only (Server)
        if (Input.GetKeyDown(KeyCode.Escape) && isServer)
        {
            CmdExitToLobby();
        }
        //Client leaves the server.
        else if(Input.GetKeyDown(KeyCode.Escape) && isClient)
        {
            NetworkManager.singleton.StopClient();
        }
      
    }

    void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(moveX * Time.deltaTime * rigidBodySpeed, moveY * Time.deltaTime * rigidBodySpeed);

        //playerRigidBody.velocity = new Vector2(x * Time.deltaTime * rigidBodySpeed, y * Time.deltaTime * rigidBodySpeed);

        //transform.position += new Vector3(x * Time.deltaTime * speed, y * Time.deltaTime * speed, 0);
    }

    [Command]
    void CmdMove(float x, float y)
    {
        moveX = x;
        moveY = y;
        GetComponent<NetworkTransform>().SetDirtyBit(1); //Players are unreliable, send this packet to an unreliable channel.
    }

    [Command]
    void CmdExitToLobby() //Kicks everyone to lobby.
    {
        var lobby = NetworkManager.singleton as NetworkLobbyManager;
        NetworkManager.singleton.ServerChangeScene(lobby.lobbyScene);
    }
}
