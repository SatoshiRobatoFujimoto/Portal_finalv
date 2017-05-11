// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity.SpatialMapping
{
    public class ObjectSurfaceObserver : SpatialMappingSource
    {
        [Tooltip("The room model to use when loading meshes in Unity.")]
        public GameObject fal;
        public GameObject plafon;

        public GameObject cube;

        public GameObject room;

        [Tooltip("If greater than or equal to zero, surface objects will claim to be updated at this period. This is useful when working with libraries that respond to updates (such as the SpatialUnderstanding library). If negative, surfaces will not claim to be updated.")]
        public float SimulatedUpdatePeriodInSeconds = -1;

        // Use this for initialization.
        private void Start()
        {
            if (!UnityEngine.VR.VRDevice.isPresent)
            {
                // When in the Unity editor and not remoting, try loading saved meshes from a model.

                //Nem működik még, meg kéne csinálni
                LoadFolyoso(fal, plafon);

                //Load(room);

                if (GetMeshFilters().Count > 0)
                {
                    SpatialMappingManager.Instance.SetSpatialMappingSource(this);
                }
            }
        }

        public void Load(GameObject roomModel)
        {
            if (roomModel == null)
            {
                Debug.Log("No room model specified.");
                return;
            }

            GameObject roomObject = Instantiate(roomModel);
            roomObject.gameObject.layer = 0;
            Cleanup();

            try
            {
                MeshFilter[] roomFilters = roomObject.GetComponentsInChildren<MeshFilter>();

                for (int iMesh = 0; iMesh < roomFilters.Length; iMesh++)
                {
                    AddSurfaceObject(CreateSurfaceObject(
                        mesh: roomFilters[iMesh].sharedMesh,
                        objectName: "roomMesh-" + iMesh,
                        parentObject: transform,
                        meshID: iMesh
                        ));
                }
            }
            catch
            {
                Debug.Log("Failed to load object " + roomModel.name);
            }
            finally
            {
                if (roomObject != null)
                {
                    DestroyImmediate(roomObject);
                }
            }
        }



        /// <summary>
        /// Loads the SpatialMapping mesh from the specified room object.
        /// </summary>
        /// <param name="roomModel">The room model to load meshes from.</param>
        public void LoadFolyoso(GameObject falModel, GameObject plafonModel)
        {
            if (falModel == null || plafonModel == null)
            {
                Debug.Log("No room model specified.");
                return;
            }

            GameObject falObject = Instantiate(falModel);
            GameObject plafonObject = Instantiate(plafonModel);

            Cleanup();

            try
            {
                MeshFilter[] falFilters = falObject.GetComponentsInChildren<MeshFilter>();
                MeshFilter[] plafonFilters = plafonObject.GetComponentsInChildren<MeshFilter>();
                Debug.Log(falFilters.Length);

                for (int iMesh = 0; iMesh < falFilters.Length; iMesh++)
                {
                    AddSurfaceObject(CreateSurfaceObject(
                        mesh: falFilters[iMesh].sharedMesh,
                        objectName: "roomMesh-" + iMesh,
                        parentObject: transform,
                        meshID: iMesh
                        ));
                    
                }
                
                for (int iMesh = 0; iMesh < plafonFilters.Length; iMesh++)
                {
                    AddSurfaceObject(CreateSurfaceObject(
                        mesh: plafonFilters[iMesh].sharedMesh,
                        objectName: "roomMesh-" + iMesh,
                        parentObject: transform,
                        meshID: iMesh
                        ));
                }
                
            }
            catch
            {
                Debug.Log("Failed to load object " + falObject.name + " or " + plafonObject.name);
            }
            finally
            {
                
                if (falObject != null)
                {
                    DestroyImmediate(falObject);
                }
                if (plafonObject != null)
                {
                    DestroyImmediate(plafonObject);
                }
                this.gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                this.transform.GetChild(1).transform.position = new Vector3(0, -1.5f, 0);
                this.transform.GetChild(0).gameObject.layer = 31;
                this.transform.GetChild(1).gameObject.layer = 31;
                //cube.gameObject.GetComponent<CubeCommands>().setMaterial();



            }
        }

        private float lastUpdateUnscaledTimeInSeconds = 0;

        private void Update()
        {
            if (SimulatedUpdatePeriodInSeconds >= 0)
            {
                if ((Time.unscaledTime - lastUpdateUnscaledTimeInSeconds) >= SimulatedUpdatePeriodInSeconds)
                {
                    for (int iSurface = 0; iSurface < SurfaceObjects.Count; iSurface++)
                    {
                        UpdateOrAddSurfaceObject(SurfaceObjects[iSurface]);
                    }

                    lastUpdateUnscaledTimeInSeconds = Time.unscaledTime;
                }
            }
        }
    }
}