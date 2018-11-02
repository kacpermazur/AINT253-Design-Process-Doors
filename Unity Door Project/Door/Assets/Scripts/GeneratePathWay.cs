using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePathWay : MonoBehaviour
{

	[SerializeField] private GameObject _instanceOFCord;

	[SerializeField] private float _amountOFBlocksSide;
	[SerializeField] private float _amountOFBlocksFoward;
	[SerializeField] private float _offset = 0.35f;
	


	void Awake()
	{
		for (int i = 0; i < _amountOFBlocksSide; i++)
		{
			for (int j = 0; j < _amountOFBlocksFoward; j++)
			{
				Instantiate(_instanceOFCord, new Vector3(-4 + i * _offset , 0 , j * -_offset - 5.35f), Quaternion.identity);
			}
		}

	}
	

}