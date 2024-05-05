using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{

    [SerializeField] private Button serveButton;
    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;

    private void Awake()
    {
        serveButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        HostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        ClientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }
}
