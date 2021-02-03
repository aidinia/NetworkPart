using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Discovery
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/NetworkDiscoveryHUD")]
    [HelpURL("https://mirror-networking.com/docs/Articles/Components/NetworkDiscovery.html")]
    [RequireComponent(typeof(NetworkDiscovery))]
    public class MirrorNetworkDiscovery : NetworkDiscoveryHUD
    {
        readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
        NetworkManager manager;
        public Button create;
        public Button find;
        private Button thisResult;
        private ServerResponse infos;
        public Canvas results;
        bool looking = false;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (networkDiscovery == null)
            {
                networkDiscovery = GetComponent<NetworkDiscovery>();
                UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);
                UnityEditor.Undo.RecordObjects(new Object[] { this, networkDiscovery }, "Set NetworkDiscovery");
            }
        }
#endif


        void OnGUI()
        {
            if (NetworkManager.singleton == null)
                return;

            if (NetworkServer.active || NetworkClient.active)
                return;

            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
            {
                if (looking)
                {
                    DrawGUI();
                }
            }
        }

        private void Awake()
        {
            manager = GetComponent<NetworkManager>();
        }
        void Start()
        {
            create.onClick.AddListener(onClickCreate);
            find.onClick.AddListener(onClickFind);
            // thisResult.onClick.AddListener(onClickConnect);


        }

        void DrawGUI()
        {
            Debug.Log($"Looking-");
            foreach (ServerResponse info in discoveredServers.Values)
            {

                thisResult = Instantiate(create, (find.transform.position - new Vector3(0, 10, 0)), find.transform.rotation, results.transform);
                thisResult.GetComponentInChildren<Text>().text = info.EndPoint.Address.ToString();
                infos = info;
                Debug.Log(info.EndPoint.Address.ToString());
                if (thisResult)
                {
                    Connect(info);
                    Debug.Log($"Connected to {info}");
                }
            }
        }

        void Connect(ServerResponse info)
        {
            NetworkManager.singleton.StartClient(info.uri);
            find.interactable = false;
            Debug.Log($"Connected to {info}");

            looking = false;
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }

        public void onClickFind()
        {
            discoveredServers.Clear();
            networkDiscovery.StartDiscovery();
            looking = true;
        }
        public void onClickConnect()
        {


        }
        public void onClickCreate()
        {
            discoveredServers.Clear();

            manager.StartHost();
            networkDiscovery.AdvertiseServer();
            create.GetComponentInChildren<Text>().text = "Stop server";
            create.onClick.RemoveAllListeners();
            create.onClick.AddListener(onClickStop);

            Debug.Log($"Created-");
        }

        public void onClickStop()
        {
            manager.StopHost();
        }

    }
}
