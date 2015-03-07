using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class FixedCube : MonoBehaviour {
    //public static SerialPort sp;
	// Use this for initialization
	void Start () {
	    //sp = GrabbingHand.sp;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    { 
        /*if(collision.gameObject.tag.Equals("Index"))
        {
            try
            {
                sp.Write(new Byte[1] { 0xF0 }, 0, 1);
                print("ion");
            }
            catch (System.InvalidOperationException)
            {
                print("cant find com port");
            }
        }
        if (collision.gameObject.tag.Equals("Thumb"))
        {
            try
            {
                sp.Write(new Byte[1] { 0x0F }, 0, 1);
                print("ton");
            }
            catch (System.InvalidOperationException)
            {
                print("cant find com port");
            }
        }*/
    }

    void OnCollisionExit(Collision collision)
    {
        /*if (collision.gameObject.tag.Equals("Index"))
        {
            try
            {
                sp.Write(new Byte[1] { 0x10 }, 0, 1);
                print("ioff");
            }
            catch (System.InvalidOperationException)
            {
                print("cant find com port");
            }
        }
        if (collision.gameObject.tag.Equals("Thumb"))
        {
            try
            {
                sp.Write(new Byte[1] { 0x01 }, 0, 1);
                print("toff");
            }
            catch (System.InvalidOperationException)
            {
                print("cant find com port");
            }
        }*/
    }
}
