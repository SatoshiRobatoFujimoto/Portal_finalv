// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using HoloToolkit.Unity;


namespace HoloToolkit.Examples.SharingWithUNET
{
    /// <summary>
    /// For a UWP application this should allow us to send or receive data given a server IP address.
    /// </summary>
    public class GenericNetworkTransmitter : Singleton<GenericNetworkTransmitter>
    {

        [Tooltip("The connection port on the machine to use.")]
        public int SendConnectionPort = 11000;

        /// <summary>
        /// When data arrives, this event is raised.
        /// </summary>
        /// <param name="data">The data that arrived.</param>
        public delegate void OnDataReady(byte[] data);
        public event OnDataReady dataReadyEvent;

        /// <summary>
        /// The server to connect to when data is needed.
        /// </summary>
        private string serverIP;

        /// <summary>
        /// Tracks if we have a connection request outstanding.
        /// </summary>
        private bool waitingForConnection = false;

        /// <summary>
        /// Keeps the most recent data buffer.
        /// </summary>
        private byte[] mostRecentDataBuffer;

        /// <summary>
        /// If someone connects to us, this is the data we will send them.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(byte[] data)
        {
            mostRecentDataBuffer = data;
        }

        /// <summary>
        /// Tells us who to contact if we need data.
        /// </summary>
        /// <param name="ServerIP"></param>
        public void SetServerIP(string ServerIP)
        {
            serverIP = ServerIP.Trim();
        }

        /// <summary>
        /// Requests data from the server and handles getting the data and firing
        /// the dataReadyEvent.
        /// </summary>
        public void RequestAndGetData()
        {
            ConnectListener();
        }

        // A lot of the work done in this class can only be done in UWP. The editor is not a UWP app.

    public void ConfigureAsServer()
    {
        Debug.Log("This script is not intended to be run from the Unity Editor");
        // In order to avoid compiler warnings in the Unity Editor we have to access a few of our fields.
        Debug.Log(string.Format("serverIP = {0} waitingForConnection = {1} mostRecentDataBuffer = {2}", serverIP, waitingForConnection, mostRecentDataBuffer == null ? "No there" : "there"));
    }
    private void ConnectListener() {}

    }
}