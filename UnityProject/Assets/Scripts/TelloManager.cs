using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TelloManager : MonoBehaviour
{
    private NetworkManager.UdpSender commandSender;
    private NetworkManager.UdpListener responseListener;
    private NetworkManager.UdpListener videoListener;
    private const string telloAddress = "192.168.10.1";
    private const int telloCommandPort = 8889;
    //private const int telloResponsePort = 8890;


    private const int telloVideoPort = 11111;


    private CancellationTokenSource cancelTokens;
    private Text responseLabel;
    private Text flightDataLabel;
    private CancellationToken token;
    private RawImage videoFrame;
    private Texture2D videoTexture;
    //private bool streaming = false;

    void Start()
    {
        commandSender = new NetworkManager.UdpSender();
        videoListener = new NetworkManager.UdpListener(telloVideoPort);
        cancelTokens = new CancellationTokenSource();
        responseLabel = GameObject.Find("responseLabel").GetComponent<Text>();
        flightDataLabel = GameObject.Find("flightDataLabel").GetComponent<Text>();
        videoFrame = GameObject.Find("videoFrame").gameObject.GetComponent<RawImage>();
       
        token = cancelTokens.Token;

        Task.Factory.StartNew(async () => {

            while (true)
            {
                try
                {
                    NetworkManager.Received received = await commandSender.Receive();
                    responseLabel.text = Encoding.Default.GetString(received.bytes);

                }
                catch (Exception ex)
                {
                    responseLabel.text = ex.Message;
                }
            }
        }, token);
    }

    public void Command()
    {
        commandSender.ConnectTo(telloAddress, telloCommandPort);
        int bytesSent = commandSender.Send("command");
    }

    public void TakeOff() => commandSender.Send("takeoff");
    public void Land() => commandSender.Send("land");
    public void StreamOff() => commandSender.Send("streamoff");

    public void StreamOn() 
    {
        Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                try
                {
                    NetworkManager.Received received = await commandSender.Receive();

                }
                catch (Exception ex)
                {
                    flightDataLabel.text = ex.Message;
                }
            }
        }, token);

        commandSender.Send("streamon");
    }
}
