using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

	[SerializeField] private List<GameObject> _doorCubes;
	[SerializeField] private GameObject _centerCube;
	private Vector3 _centerCubePos;

	[SerializeField] private List<Vector3> _OriginalCubePos;
	[SerializeField] private List<Vector3> _pointsToPathWay;

	[Header("Door Values")]
	[SerializeField] private float _sinSpeed;
	[SerializeField] private float _sinDistance;
	[SerializeField] private float _blockMoveSpeed = 0.1f;

	private BoxCollider _boxCol;
	
	private AudioSource _audioSrc;
	[SerializeField] private AudioClip _sOpen;
	[SerializeField] private AudioClip _sClose;

	private enum State
	{
		idle,
		open,
		close
	}
	private State _door;

	void Awake()
	{
		for (int i = 0; i < _doorCubes.Count; i++)
		{
			_OriginalCubePos.Add(_doorCubes[i].transform.position);
		}

		_boxCol = GetComponent<BoxCollider>();
		_audioSrc = GetComponent<AudioSource>();
		
		_door = State.idle;
	}

	void Start()
	{
		foreach (GameObject path in GameObject.FindGameObjectsWithTag("PathWay"))
		{
			_pointsToPathWay.Add(path.transform.position);
		}

		if (_centerCube != null)
		{
			_centerCubePos = _centerCube.transform.position;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (_door == State.idle)
			{
				_audioSrc.PlayOneShot(_sOpen, 0.7F);
				_boxCol.enabled = false;
				_door = State.open;
			}
			else if (_door == State.open)
			{
				_audioSrc.PlayOneShot(_sClose, 0.7F);
				_boxCol.enabled = true;
				_door = State.close;
			}
			else if (_door == State.close)
			{
				_door = State.idle;
			}
		}
	}

	void FixedUpdate()
	{
		for (int i = 0; i < _doorCubes.Count; i++)
		{
			if (_door == State.idle)
			{
				Idle(i);
			}
			else if (_door == State.open)
			{
				Open(i);
			}
			else if (_door == State.close)
			{
				Close(i);
			}
		}
	}

	private void Idle(int currentCube)
	{
		float cubeDistance = Vector3.Distance(_centerCubePos, new Vector3(_doorCubes[currentCube].transform.position.x, _doorCubes[currentCube].transform.position.y, 0F));

		float sinValue = Mathf.Sin(cubeDistance * _sinSpeed * Time.time);
		float sinDistance = sinValue * _sinDistance;

		Vector3 calculatedValue = _doorCubes[currentCube].transform.position;
		calculatedValue.z += sinDistance;

		_doorCubes[currentCube].transform.position = calculatedValue;
	}

	private void Open(int currentCube)
	{
		_doorCubes[currentCube].transform.position = Vector3.MoveTowards(_doorCubes[currentCube].transform.position,
			_pointsToPathWay[currentCube], _blockMoveSpeed);
		
	}

	private void Close(int currentCube)
	{
		_doorCubes[currentCube].transform.position = Vector3.MoveTowards(_doorCubes[currentCube].transform.position,
			_OriginalCubePos[currentCube], _blockMoveSpeed);
	}
}
