using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TelloLib;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using InControl;

public class TelloManager : SingletonMonoBehaviour<TelloManager> {

	private static bool isLoaded = false;

	private TelloVideoTexture telloVideoTexture;
	private PlayerActions playerActions;
	private InputDevice inputDevice;
	private GatlingGun gatlingGun;

	public enum FlipType  // FlipType is used for the various flips supported by the Tello.
	{
		FlipFront = 0, // FlipFront flips forward.
		FlipLeft = 1, // FlipLeft flips left.
		FlipBack = 2, // FlipBack flips backwards.
		FlipRight = 3, // FlipRight flips to the right.
		FlipForwardLeft = 4, // FlipForwardLeft flips forwards and to the left.
		FlipBackLeft = 5, // FlipBackLeft flips backwards and to the left.
		FlipBackRight = 6, // FlipBackRight flips backwards and to the right.
		FlipForwardRight = 7, // FlipForwardRight flips forewards and to the right.
	};
	public enum VideoBitRate // VideoBitRate is used to set the bit rate for the streaming video returned by the Tello.
	{
		VideoBitRateAuto = 0, // VideoBitRateAuto sets the bitrate for streaming video to auto-adjust.
		VideoBitRate1M = 1, // VideoBitRate1M sets the bitrate for streaming video to 1 Mb/s.
		VideoBitRate15M = 2, // VideoBitRate15M sets the bitrate for streaming video to 1.5 Mb/s
		VideoBitRate2M = 3, // VideoBitRate2M sets the bitrate for streaming video to 2 Mb/s.
		VideoBitRate3M = 4, // VideoBitRate3M sets the bitrate for streaming video to 3 Mb/s.
		VideoBitRate4M = 5, // VideoBitRate4M sets the bitrate for streaming video to 4 Mb/s.
	};

	override protected void Awake()
	{
		if (!isLoaded) {
			DontDestroyOnLoad(this.gameObject);
			isLoaded = true;
		}
		base.Awake();

		Tello.onConnection += Tello_onConnection;
		Tello.onUpdate += Tello_onUpdate;
		Tello.onVideoData += Tello_onVideoData;

		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 20;

		gatlingGun = GameObject.Find("GatlingGun").gameObject.GetComponent<GatlingGun>(); ;

		playerActions = PlayerActions.CreateWithDefaultBindings();

		if (telloVideoTexture == null)
			telloVideoTexture = FindObjectOfType<TelloVideoTexture>();
	}

	private void OnEnable()
	{
		if (telloVideoTexture == null)
			telloVideoTexture = FindObjectOfType<TelloVideoTexture>();
	}

	private void Start()
	{
		if (telloVideoTexture == null)
			telloVideoTexture = FindObjectOfType<TelloVideoTexture>();

		Tello.startConnecting();
	}

	void OnApplicationQuit()
	{
		Tello.stopConnecting();
	}

	// Update is called once per frame
	void Update () {

		inputDevice = InputManager.ActiveDevice;

		float leftX = inputDevice.LeftStick.X;
		float leftY = inputDevice.LeftStick.Y;
		float rightX = inputDevice.RightStick.X;
		float rightY = inputDevice.RightStick.Y;

		if (playerActions.takeoff.WasPressed)
		{
			Tello.takeOff();
			Debug.Log("takeoff");
		}
		else if(playerActions.land.WasPressed)
		{
			Tello.land();
			Debug.Log("land");
		}
		else if (playerActions.fire.IsPressed)
		{
			gatlingGun.Fire();
			Debug.Log("fire");
		}


		


		//Debug.Log(String.Format("leftX: {0} ,leftY: {1} , rightX: {2} ,rightY: {3} ", leftX, leftY, rightX, rightY));
		Tello.controllerState.setAxis(rightX, leftY, leftX, rightY);
	}

	private void Tello_onUpdate(int cmdId)
	{
		//Debug.Log("Tello_onUpdate : " + Tello.state);
	}

	private void Tello_onConnection(Tello.ConnectionState newState)
	{

		if (newState == Tello.ConnectionState.Connected) {
            Tello.queryAttAngle();
            Tello.setMaxHeight(50);

			Tello.setPicVidMode(1); // 0: picture, 1: video
			Tello.setVideoBitRate((int)VideoBitRate.VideoBitRate1M);
			//Tello.setEV(0);
			Tello.requestIframe();
		}
	}

	private void Tello_onVideoData(byte[] data)
	{
		//Debug.Log("Tello_onVideoData: " + data.Length);
		if (telloVideoTexture != null)
			telloVideoTexture.PutVideoData(data);
	}

}
