﻿using UnityEngine;
using System.Collections;

public class IndexFinger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag.Equals ("ignoreCollision")) {
			print ("ignoring collision");
			return;
		}
		RigidbodyConstraints objectConstraints = collision.gameObject.rigidbody.constraints;
		if (objectConstraints == RigidbodyConstraints.FreezePositionX | objectConstraints == RigidbodyConstraints.FreezePositionY | objectConstraints == RigidbodyConstraints.FreezePositionZ | objectConstraints == RigidbodyConstraints.FreezeAll | objectConstraints == RigidbodyConstraints.FreezePosition) {
			CommController.freezeIndex ();
		} else {
			// Resistance value needs to be between 0 and )
			double resistance = 1.0 - (1.0/collision.gameObject.rigidbody.mass);
			if (resistance > 1.0 | resistance < 0.0) {
				resistance = 0.0;
			}
			print(resistance);
			CommController.resistanceIndex(resistance);
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag.Equals ("ignoreCollision")) {
			return;
		}
		CommController.releaseIndex ();
	}
}
