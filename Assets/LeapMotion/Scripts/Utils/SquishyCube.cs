using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class SquishyCube : GrabbableObject {
	
	public override void OnGrab(SerialPort sp) {
		grabbed_ = true;
		hovered_ = false;
		// Write out serial out
		try {
			sp.Write(new Byte[1] {0x22}, 0, 1);
			print ("grabbed");
		} catch (System.InvalidOperationException) {
			print("cant find com port");
        }

		if (breakableJoint != null) {
			Joint breakJoint = breakableJoint.GetComponent<Joint>();
			if (breakJoint != null) {
				breakJoint.breakForce = breakForce;
				breakJoint.breakTorque = breakTorque;
			}
		}
	}
	
	public override void OnRelease(SerialPort sp) {
		grabbed_ = false;

		try {
			sp.Write(new Byte[1] {0x11}, 0, 1);
		} catch (System.InvalidOperationException) {
			print("cant find com port");
        }
        
		
		if (breakableJoint != null) {
			Joint breakJoint = breakableJoint.GetComponent<Joint>();
			if (breakJoint != null) {
				breakJoint.breakForce = Mathf.Infinity;
                breakJoint.breakTorque = Mathf.Infinity;
            }
        }
    }
}
