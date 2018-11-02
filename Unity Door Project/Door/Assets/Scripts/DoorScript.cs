using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{

	[SerializeField] private List<GameObject> _doorCubes;

	[SerializeField] private List<Vector3> _cubeOriginalPosition;
	[SerializeField] private List<Vector3> _pathWayPoints;

	[SerializeField] private GameObject _centerCube;
	private Vector3 _centerCubePos;

	[SerializeField] private Text _text;

[Header("Door Values")]
	[SerializeField] private float _sinSpeed;
	[SerializeField] private float _sinDistance;
	[SerializeField] private float _blockMoveSpeed = 0.1f;

	public bool playerTirggerClose = false;
	

	enum States
	{
		Open,
		Opened,
		Close
	}
	
	States DoorState;
	

	void Awake()
	{
		
		
		for (int i = 0; i < _doorCubes.Count; i++)
		{
			_cubeOriginalPosition.Add(_doorCubes[i].transform.position);
		}

		DoorState = States.Close;
	}

	void Start()
    {
	    foreach (GameObject e in GameObject.FindGameObjectsWithTag("PathwayPoint"))
	    {
		    _pathWayPoints.Add(e.transform.position);
	    }
	    
	    
	    if (_centerCube != null)
	    {
		    _centerCubePos = _centerCube.transform.position;
	    }
    }


	void FixedUpdate()
	{

		for (int i = 0; i < _doorCubes.Count; i++)
		{
			if (DoorState == States.Close)
			{
				Idle(i);
			}
			else if (DoorState == States.Open)
			{
				Open(i);
				DoorState = States.Opened;
			}
			else if (DoorState == States.Opened && playerTirggerClose)
			{
				Close(i);
				DoorState = States.Close;
			}
			else
			{
				Debug.Log("something broke");
			}
		}
	}


	private void Idle(int currentCube)
	{
		float cubeDistance = Vector3.Distance(_centerCubePos,
			new Vector3(_doorCubes[currentCube].transform.position.x, _doorCubes[currentCube].transform.position.y, 0F));

		float sinValue = Mathf.Sin(cubeDistance * _sinSpeed * Time.time);
		float sinDistance = sinValue * _sinDistance;

		Vector3 calcuatedValue = _doorCubes[currentCube].transform.position;
		calcuatedValue.z += sinDistance;

		_doorCubes[currentCube].transform.position = calcuatedValue;
	}

	private void Open(int e)
	{
		_doorCubes[e].transform.position = Vector3.MoveTowards(_doorCubes[e].transform.position, _pathWayPoints[e], _blockMoveSpeed);
	}

	private void Close(int e)
	{
		_doorCubes[e].transform.position = Vector3.MoveTowards(_doorCubes[e].transform.position, _cubeOriginalPosition[e], _blockMoveSpeed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_text.enabled = true;

			if (Input.GetKeyDown(KeyCode.E))
			{
				DoorState = States.Open;
			}

		}
	}
}
