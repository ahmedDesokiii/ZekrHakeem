using System.Collections.Concurrent;

namespace ZekrHakeem_v11.Services.Security
{
    /// <summary>تتبع المستخدمين المتصلين الآن (عدد الاتصالات لكل مستخدم)</summary>
    public class PresenceTracker
    {
        private readonly ConcurrentDictionary<string, int> _connections = new();

        /// <returns>true لو كان أول اتصال (أصبح متصلاً الآن)</returns>
        public bool Connected(string userId)
        {
            var count = _connections.AddOrUpdate(userId, 1, (_, c) => c + 1);
            return count == 1;
        }

        /// <returns>true لو كان آخر اتصال (أصبح غير متصل)</returns>
        public bool Disconnected(string userId)
        {
            while (_connections.TryGetValue(userId, out var c))
            {
                if (c <= 1)
                {
                    if (_connections.TryRemove(new KeyValuePair<string, int>(userId, c))) return true;
                }
                else if (_connections.TryUpdate(userId, c - 1, c)) return false;
            }
            return false;
        }

        public bool IsOnline(string userId) => _connections.ContainsKey(userId);
        public IReadOnlyCollection<string> OnlineUserIds => _connections.Keys.ToList();
        public int OnlineCount => _connections.Count;
    }
}
