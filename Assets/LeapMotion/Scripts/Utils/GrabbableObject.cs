/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class GrabbableObject : MonoBehaviour {

  public bool useAxisAlignment = false;
  public Vector3 rightHandAxis;
  public Vector3 objectAxis;

  public bool rotateQuickly = true;
  public bool centerGrabbedObject = false;

  public Rigidbody breakableJoint;
  public float breakForce;
  public float breakTorque;

  protected bool grabbed_ = false;
  protected bool hovered_ = false;

  public bool IsHovered() {
    return hovered_;
  }

  public bool IsGrabbed() {
    return grabbed_;
  }

  public virtual void OnStartHover() {
    hovered_ = true;
  }

  public virtual void OnStopHover() {
    hovered_ = false;
  }

  public virtual void OnGrab(SerialPort sp) {
    grabbed_ = true;
    hovered_ = false;
	
	// Write out serial out
	try {
		sp.Write(new Byte[1] {0xFF}, 0, 1);
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

  public virtual void OnRelease(SerialPort sp) {
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
