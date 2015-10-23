using UnityEngine;
using System.Collections;

public class TestPlayerController : MonoBehaviour {

    public float speed = 1;
    public float rigidBodySpeed = 100;

    float x;
    float y;
    Transform playerTransform;
    Rigidbody2D playerRigidBody;

	// Use this for initialization
	void Start () {
        playerTransform = GetComponent<Transform>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
	}

    void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(x * Time.deltaTime * rigidBodySpeed, y * Time.deltaTime * rigidBodySpeed);
        //transform.position += new Vector3(x * Time.deltaTime * speed, y * Time.deltaTime * speed, 0);
    }
}
