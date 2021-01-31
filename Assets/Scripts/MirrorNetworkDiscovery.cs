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
        Vector2 scrollViewPos = Vector2.zero;

      
        public Button create;
        public Button find;
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

        //public MirrorNetworkDiscovery(Button c, Button f, Canvas r)
        //{
        //    create = c;
        //    find = f;
        //    results = r;
        //}

        void OnGUI()
        {
            if (NetworkManager.singleton == null)
                return;

            if (NetworkServer.active || NetworkClient.active)
                return;

            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
                if (looking)
                    DrawGUI();
        }

        void Start()
        {
            create.onClick.AddListener(onClickCreate);
            find.onClick.AddListener(onClickFind);
        }

        void DrawGUI()
        {
          
            foreach (ServerResponse info in discoveredServers.Values)
            {
                Button thisResult = Instantiate(create, results.transform);
                thisResult.GetComponentInChildren<Text>().text = info.EndPoint.Address.ToString();
                Debug.Log(info.EndPoint.Address.ToString());
                if (thisResult)
                    Connect(info);
            }

        }

        void Connect(ServerResponse info)
        {
            NetworkManager.singleton.StartClient(info.uri);
            looking = false;
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }

        public void onClickFind() {
            discoveredServers.Clear();
            networkDiscovery.StartDiscovery();
            Debug.Log($"Looking-");
            looking = true;
            DrawGUI();
        }
        public void onClickCreate()
        {
            discoveredServers.Clear();
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();

            Debug.Log($"Created-");
        }
    }
}
