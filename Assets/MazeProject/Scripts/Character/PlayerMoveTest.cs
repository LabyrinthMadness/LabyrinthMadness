using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest : MonoBehaviour {
    Rigidbody2D r2b;

	// Use this for initialization
	void Start () {
        r2b = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(x,y);

        r2b.AddForce(movement * 1.0f);
    }
}
