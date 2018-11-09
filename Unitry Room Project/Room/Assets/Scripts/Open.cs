using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Open : MonoBehaviour {
	
	private Animator _anim;
	private AudioSource _sound;
	
	[SerializeField] private Text _text;

	private bool _canPlayerInteract;
	private bool _isOpen;

	void Start ()
	{
		_canPlayerInteract = false;
		_isOpen = false;
		
		_anim = GetComponent<Animator>();
		_sound = GetComponent<AudioSource>();
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.E) && _canPlayerInteract)
		{
			_sound.Play();
			
			_isOpen = !_isOpen;
		}
		
		_anim.SetBool("isOpen", _isOpen);
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
