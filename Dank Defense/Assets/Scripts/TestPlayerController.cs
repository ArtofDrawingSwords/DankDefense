using UnityEngine;
using System.Collections;

public class TestPlayerController : MonoBehaviour {

    public float speed = 1;

    float x;
    float y;
    Transform playerTransform;

	// Use this for initialization
	void Start () {
        playerTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
	}

    void FixedUpdate()
    {
        transform.position += new Vector3(x * Time.deltaTime * speed, y * Time.deltaTime * speed, 0);
    }
}
