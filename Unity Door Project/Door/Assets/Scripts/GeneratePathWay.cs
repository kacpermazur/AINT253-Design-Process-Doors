using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePathWay : MonoBehaviour
{

    [SerializeField] private GameObject _instanceOfPos;


    [SerializeField] private float _amountOFBlocksSide;
    [SerializeField] private float _amountOFBlocksFoward;
    [SerializeField] private float _offset = 0.35f;

    void Awake()
    {
        for (int i = 0; i < _amountOFBlocksSide; i++)
        {
            for (int j = 0; j < _amountOFBlocksFoward; j++)
            {
                Instantiate(_instanceOfPos, new Vector3(-10F + i * _offset, 0F, j * -_offset - 6F), Quaternion.identity);
            }
        }
    }
}