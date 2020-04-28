using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button commandButton;
    private Button takeoffButton;
    private Button landButton;
    private Button streamOnButton;
    private Button streamOffButton;
    private TelloManager manager;

    void Start()
    {
        manager = GameObject.Find("Scripts").GetComponent<TelloManager>();

        commandButton = GameObject.Find("command").GetComponent<Button>();
        takeoffButton = GameObject.Find("takeoff").GetComponent<Button>();
        landButton = GameObject.Find("land").GetComponent<Button>();
        streamOnButton = GameObject.Find("streamon").GetComponent<Button>();

        commandButton.onClick.AddListener(commandOnClick);
        takeoffButton.onClick.AddListener(takeoffOnClick);
        landButton.onClick.AddListener(landOnClick);
        streamOnButton.onClick.AddListener(streamOnClick);
    }

    void commandOnClick()
    {
        manager.Command();
    }

    void takeoffOnClick()
    {
        manager.TakeOff();
    }

    void landOnClick()
    {
        manager.Land();
    }

    void streamOnClick()
    {
        manager.StreamOn();
    }

}
