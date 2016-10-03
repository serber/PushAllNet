using PushAll.Models;

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
        ulong SendMulticastAsync(MulticastParameters parameters);

        /// <summary>
        /// Send broadcast PUSH notification
        /// </summary>
        ulong SendBroadcastAsync(PushParameters parameters);

        /// <summary>
        /// Send unicast PUSH notification
        /// </summary>
        ulong SendUnicastAsync(UnicastParameters parameters);
    }
}