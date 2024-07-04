using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Toggle ipV6Toogle;
    public TMP_InputField IpAdressInput;
    public bool usingIpV6 = false;

    private string IpAdr = "127.0.0.1";
    private ushort Port = 7777;
    

    private UnityTransport unityTransport;

    private void Start()
    {
        IpAdressInput.onValueChanged.AddListener((e)=> { ValueChangeCheck(e); });
        usingIpV6 = ipV6Toogle.isOn;
        ipV6Toogle.onValueChanged.AddListener(delegate {
            usingIpV6 = !usingIpV6;
        });

        IpAdressInput.text = IpAdr;

        unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        Cursor.visible = true;
    }
    public void ValueChangeCheck(string e)
    {
        IpAdr = e;
    }

    private void Awake()
    {
        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            Cursor.visible = false;
            gameObject.SetActive(false);
        });
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            Cursor.visible = false;
            gameObject.SetActive(false);
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkFamily IpMode = usingIpV6 ? NetworkFamily.Ipv6 : NetworkFamily.Ipv4;
            NetworkEndPoint endPoint = NetworkEndPoint.Parse(IpAdr, Port, IpMode);

            unityTransport.SetConnectionData(endPoint);
            NetworkManager.Singleton.StartClient();
            Cursor.visible = false;
            gameObject.SetActive(false);
        });
    }
}
