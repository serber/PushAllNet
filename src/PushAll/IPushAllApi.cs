using PushAll.Models;
using System.Threading.Tasks;

namespace PushAll
{
    /// <summary>
    /// PushAll API client interface
    /// </summary>
    public interface IPushAllApi
    {
        /// <summary>
        /// Send multicast PUSH notification
        /// </summary>
        Task<ulong> SendMulticastAsync(MulticastParameters parameters);

        /// <summary>
        /// Send broadcast PUSH notification
        /// </summary>
        Task<ulong> SendBroadcastAsync(PushParameters parameters);

        /// <summary>
        /// Send unicast PUSH notification
        /// </summary>
        Task<ulong> SendUnicastAsync(UnicastParameters parameters);
    }
}