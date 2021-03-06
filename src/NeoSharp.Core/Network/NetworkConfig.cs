﻿using Microsoft.Extensions.Configuration;

namespace NeoSharp.Core.Network
{
    public class NetworkConfig
    {
        /// <summary>
        /// Magic number
        /// </summary>
        public uint Magic { get; internal set; }
        /// <summary>
        /// Portt
        /// </summary>
        public ushort Port { get; internal set; }
        /// <summary>
        /// Force Ipv6
        /// </summary>
        public bool ForceIPv6 { get; internal set; }
        /// <summary>
        /// Peers
        /// </summary>
        public EndPoint[] PeerEndPoints { get; internal set; }
        /// <summary>
        /// ACL Config
        /// </summary>
        public NetworkACLConfig ACL { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public NetworkConfig(IConfiguration configuration = null)
        {
            PeerEndPoints = new EndPoint[0];
            configuration?.GetSection("network")?.Bind(this);
        }
    }
}