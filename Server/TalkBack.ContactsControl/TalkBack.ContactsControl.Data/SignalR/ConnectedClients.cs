using DM.DTOs;

namespace TalkBack.ContactsControl.Data.SignalR
{
    public class ConnectedClients
    {
        private static ConnectedClients _instance;
        private Dictionary<Guid, string> _connectedClientsDic;
        private List<ConnectedClientEntity> _connectedClientsList;
        public List<string> _connectedClientsIds;
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
            _connectedClientsIds = new List<string>();
            _connectedClientsList = new List<ConnectedClientEntity>();
        }
        public List<ConnectedClientEntity> GetAllConnectedUsers()
        {
            return _connectedClientsList;
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
            return _connectedClientsIds.Contains(connectionId);
        }

        public void OnClientConnected(string connectionId)
        {
            if (!CheckIfConnected(connectionId))
            {
                lock (_disconnectedClientsIdsLock)
                {
                    if (!CheckIfConnected(connectionId))
                    {
                        _connectedClientsIds.Add(connectionId);
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
                        _connectedClientsIds.Remove(connectionId);
                    }
                }
            }
        }
        public bool AddOrUpdateConnectionId(Guid id, string connectionId, string name)
        {
            bool success;
            if (!_connectedClientsDic.ContainsKey(id))
            {
                ConnectedClientEntity cce = new ConnectedClientEntity(id, connectionId, name);

                _connectedClientsList.Add(cce);
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

