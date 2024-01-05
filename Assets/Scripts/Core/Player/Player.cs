using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Player
{
	public class Player : MonoBehaviour, IPlayer
	{
		[SerializeField] private float speed;
    
		[SerializeField] [HideInInspector] 
		private Rigidbody rigidbody;

		public GameObject GameObject => gameObject;
		
		private void OnValidate()
		{
			rigidbody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			Movement();
		}

		private void Movement()
		{
			var input = GetInput();

			rigidbody.velocity = new Vector3(input.x * speed, rigidbody.velocity.y, input.y * speed);
		}
    
		private Vector2 GetInput() =>
			new(Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"));
		
		public class Factory : PlaceholderFactory<Player>
		{ }
	}   
}
