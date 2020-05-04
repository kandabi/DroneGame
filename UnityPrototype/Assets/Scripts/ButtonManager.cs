using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button connectButton;
    private Button disconnectButton;
    //private Button takeoffButton;
    //private Button landButton;
    private Button streamOnButton;
    //private Button streamOffButton;
    private TelloManager manager;

    void Start()
    {
        //manager = GameObject.Find("Scripts").GetComponent<TelloManager>();
        manager = TelloManager.Instance;

        connectButton = GameObject.Find("connectButton").GetComponent<Button>();
        disconnectButton = GameObject.Find("disconnectButton").GetComponent<Button>();
        //takeoffButton = GameObject.Find("takeoff").GetComponent<Button>();
        //landButton = GameObject.Find("land").GetComponent<Button>();
        streamOnButton = GameObject.Find("streamon").GetComponent<Button>();

        connectButton.onClick.AddListener(connectOnClick);
        disconnectButton.onClick.AddListener(disconnectOnClick);
        //takeoffButton.onClick.AddListener(takeoffOnClick);
        //landButton.onClick.AddListener(landOnClick);
        streamOnButton.onClick.AddListener(streamOnClick);
    }

    void connectOnClick()
    {
        //manager.Connect();
    }

    void disconnectOnClick()
    {
        //manager.Disconnect();
    }

    void streamOnClick()
    {
        //manager.StreamOn();
    }

    //void takeoffOnClick()
    //{
    //    manager.TakeOff();
    //}

    //void landOnClick()
    //{
    //    manager.Land();
    //}
}
