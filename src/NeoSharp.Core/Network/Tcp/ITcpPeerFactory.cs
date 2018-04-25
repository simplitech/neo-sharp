﻿using System.Net.Sockets;

namespace NeoSharp.Core.Network.Tcp
{
    public interface ITcpPeerFactory : IPeerFactory
    {
        TcpPeer Create(Socket socket);
    }
}