using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Threading;
using Leap;

public class CommController : MonoBehaviour {

	public static string targetUnixPort = "/dev/tty.usbmodem1421";
	private static SerialPort comPort = new SerialPort(targetUnixPort, 9600, Parity.None, 8, StopBits.One);
	private static double resistanceMultiplier = 14;
	public static int massMultiplier = 1;
	public static OperatingSystem OSVersion;
	public static Byte[] indexBytes = new Byte[15] {0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90, 0xA0, 0xB0, 0xC0, 0xD0, 0xE0, 0xF0};
	public static Byte[] thumbBytes = new Byte[15] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F};

	// Use this for initialization
	void Start () {
		/*if (OSVersion.Platform.Equals(PlatformID.MacOSX) | OSVersion.Platform.Equals(PlatformID.Unix)) {
			// Do mac things
			print ("Youre on UNix!");
			comPort = new SerialPort(targetUnixPort, 9600, Parity.None, 8, StopBits.One);
		} else {
			// Do windows things
			print("Youre on Windows!");
			//string[] names = SerialPort.GetPortNames();
			//comPort = new SerialPort (names[0], 9600, Parity.None, 8, StopBits.One);
			comPort = new SerialPort(targetUnixPort, 9600, Parity.None, 8, StopBits.One);
		}*/
		if (!comPort.IsOpen) {
				OpenConnection ();
	 	}
	}

	public void OpenConnection() {
	if (comPort != null) {
		if (comPort.IsOpen) {
				comPort.Close();
				//message = "Closing port";
		} else {
				comPort.Open ();
				comPort.ReadTimeout = 50;
				//message = "Port Opened!";
		}
	} else {
		if (comPort.IsOpen) {
			print("Port is opened");
		} else {
			print("Port == null");
		}
	}
  }

	// Update is called once per frame
	void Update () {
		// Write watchdog signal to verify connection stable
		try {
			comPort.Write (new Byte[1] {0x00}, 0, 1);
		} catch (System.InvalidOperationException) {
			print ("cant find com port");
		}
	}

	// Freeze pinch state
	public static void freezePinch() {
		freezeIndex ();
		freezeThumb ();
	}

	//Release pinch state
	public static void releasePinch() {
		releaseIndex ();
		releaseThumb ();
	}

	// Send lock signal for index finger
	public static void freezeIndex() {
		int resistanceStep = 14;
		//string serialData = resistanceStep.ToString ("X4");
		print ("locking index");
		comPort.Write (new Byte[1] {indexBytes[resistanceStep]}, 0, 1);
	}

	// Send lock signal for thumb
	public static void freezeThumb() {
		int resistanceStep = 14;
		//string serialData = resistanceStep.ToString ("X4");
		print ("locking thumb");
		comPort.Write (new Byte[1] {thumbBytes[resistanceStep]}, 0, 1);
	}

	// Send Release index finger signal
	public static void releaseIndex() {
		print ("release index");
		comPort.Write (new Byte[1] {0x10}, 0, 1);
	}

	// Send Release thumb signal
	public static void releaseThumb() {
		print ("release thumb");
		comPort.Write (new Byte[1] {0x01}, 0, 1);
	}

	// Send resistance step for index finger
	// resistance ranges from 0 to 1
	public static void resistanceIndex(double resistance) {
		int resistanceStep = (int)(Math.Floor (resistance * resistanceMultiplier));
		if (resistanceStep > 1) {
			resistanceStep = 1;
		}
		//string hexValue = resistanceStep.ToString("X");
		//string serialData = resistance.ToString ("X4");
		print (resistanceStep);
		comPort.Write (new Byte[1] {indexBytes[resistanceStep]}, 0, 1);
	}

	// Send resistance step for thumb
	// resistance ranges from 0 to 1
	public static void resistanceThumb(double resistance) {
		int resistanceStep = (int) (Math.Floor (resistance * resistanceMultiplier));
		if (resistanceStep > 1) {
			resistanceStep = 1;
		}
		//string hexValue = resistanceStep.ToString("X");
		//string serialData = resistance.ToString ("X4");
		print (resistanceStep);
		comPort.Write (new Byte[1] {thumbBytes[resistanceStep]}, 0, 1);
	}
	
}
