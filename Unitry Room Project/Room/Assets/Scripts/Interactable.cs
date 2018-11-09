using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

	private Animator _anim;
	private AudioSource _sound;

	[SerializeField] private Text _text;
	[SerializeField] private GameObject[] _obj;

	private bool _canPlayerInteract;
	private bool _isOn;
	
	void Start ()
	{
		_isOn = true;
		
		_text.enabled = false;
		_canPlayerInteract = false;
		
		_anim = GetComponent<Animator>();
		_sound = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && _canPlayerInteract)
		{
			_sound.Play();
			
			_isOn = !_isOn;
			
			for (int i = 0; i < _obj.Length; i++)
			{
				_obj[i].GetComponent<Light>().enabled = !_obj[i].GetComponent<Light>().enabled;
			}
		}
		
		_anim.SetBool("isOn", _isOn);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_text.enabled = true;
			_canPlayerInteract = true;
		}
		
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_text.enabled = false;
			_canPlayerInteract = false;
		}
	}
}
