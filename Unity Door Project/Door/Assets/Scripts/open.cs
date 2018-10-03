using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open : MonoBehaviour
{
	[SerializeField]
	private float speed = 1f;

	private Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey == true)
		{
			rb.velocity = Vector3.back * speed * 1000 * Time.deltaTime;
		}
	}
}
