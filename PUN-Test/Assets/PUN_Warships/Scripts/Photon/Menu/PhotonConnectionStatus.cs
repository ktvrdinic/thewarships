using UnityEngine;
using UnityEngine.UI;

public class PhotonConnectionStatus : Photon.PunBehaviour
{

    private readonly string connectionStatusMessage = "    Connection Status: ";

    [Header("UI References")]
    public Text ConnectionStatusText;

    #region UNITY

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        ConnectionStatusText.text = connectionStatusMessage + PhotonNetwork.connectionState;
    }

    #endregion
}
