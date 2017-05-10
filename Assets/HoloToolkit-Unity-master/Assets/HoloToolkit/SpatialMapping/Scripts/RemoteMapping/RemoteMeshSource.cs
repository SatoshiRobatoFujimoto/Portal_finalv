// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;


namespace HoloToolkit.Unity.SpatialMapping
{
    /// <summary>
    /// RemoteMeshSource will try to send meshes from the HoloLens to a remote system that is running the Unity editor.
    /// </summary>
    public class RemoteMeshSource : Singleton<RemoteMeshSource>
    {
        [Tooltip("The IPv4 Address of the machine running the Unity editor. Copy and paste this value from RemoteMeshTarget.")]
        public string ServerIP;

        [Tooltip("The connection port on the machine to use.")]
        public int ConnectionPort = 11000;

    }
}
