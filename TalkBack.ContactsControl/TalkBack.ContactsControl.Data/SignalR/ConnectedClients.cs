using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.ContactsControl.Data.SignalR
{
    public class ConnectedClients
    {
        private static ConnectedClients _instance;
        private Dictionary<Guid, string> _connectedClientsDic;
        public List<string> __connectedClientsIds;
        private static readonly object _instanceLock = new object();
        private static readonly object _connectedClientsIdsLock = new object();
        private static readonly object _disconnectedClientsIdsLock = new object();

        public static ConnectedClients Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConnectedClients();
                        }
                    }
                }
                return _instance;
            }
        }
        private ConnectedClients()
        {
            _connectedClientsDic = new Dictionary<Guid, string>();
            __connectedClientsIds = new List<string>();
        }
        public string GetChatClientConnectionId(Guid id)
        {
            string connectionId = null;
            if (_connectedClientsDic.ContainsKey(id))
            {
                _connectedClientsDic.TryGetValue(id, out connectionId);
            }
            return connectionId;
        }
        public bool CheckIfConnected(string connectionId)
        {
            return __connectedClientsIds.Contains(connectionId);
        }
        public void OnClientConnected(string connectionId)
        {
            if (!CheckIfConnected(connectionId))
            {
                lock (_disconnectedClientsIdsLock)
                {
                    if (!CheckIfConnected(connectionId))
                    {
                        __connectedClientsIds.Add(connectionId);
                    }
                }
            }
        }
        public void OnClientDisconnected(string connectionId)
        {

            if (CheckIfConnected(connectionId))
            {
                lock (_disconnectedClientsIdsLock)
                {
                    if (CheckIfConnected(connectionId))
                    {
                        __connectedClientsIds.Remove(connectionId);
                    }
                }
            }
        }
        public bool AddOrUpdateConnectionId(Guid id, string connectionId)
        {
            bool success;
            if (!_connectedClientsDic.ContainsKey(id))
            {
                success = _connectedClientsDic.TryAdd(id, connectionId);
            }
            else
            {
                _connectedClientsDic[id] = connectionId;
                success = true;
            }
            return success;
        }
    }
}

