using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

	public List<GameObject> doorCubes;
	public GameObject centerCube;
	private Vector3 m_center;
	public float sinSpeed;
	public float sinDistance;


    void Awake()
    {
        m_center = centerCube.transform.position;
    }
    void Start()
	{
		
	}

	void Update()
	{
		for (int i = 0; i < doorCubes.Count; i++)
		{
			float dist = Vector3.Distance(m_center, new Vector3(doorCubes[i].transform.position.x, doorCubes[i].transform.position.y, 0.0f));
			float sinVal = Mathf.Sin(dist * sinSpeed * Time.time);
			float finalVal = sinVal * sinDistance;
			Vector3 someVal = doorCubes[i].transform.transform.position;
			someVal.z = someVal.z + finalVal;
			doorCubes[i].transform.position = someVal;
		}
	}
}
