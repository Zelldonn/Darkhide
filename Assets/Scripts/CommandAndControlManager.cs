using System.Collections.Generic;
using UnityEngine;


public class CommandAndControlManager : MonoBehaviour
{
    public RenderTexture renderTextureScreen;
    public Material screenMaterial;
    
    private List<Camera> cameras = new List<Camera>();
    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    bool AreCamerasActive = true;

    bool UpdateHasBeenDone = false;

    void Start()
    {
        FindAndGetCameras();
        FindAndGetScreens();
        if (cameras.Count < 1) { Debug.Log("Command & Control script : Unable to find cameras"); return; }
        if (meshRenderers.Count < 1) { Debug.Log("Command & Control script : Unable to find Mesh Renderer of the screens"); return; }
        CreateRenderTextures();
    }

    private void FindAndGetScreens()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Screen");
        foreach (GameObject go in gos)
        {
            meshRenderers.Add(go.GetComponent<MeshRenderer>());
        }
    }

    private void FindAndGetCameras()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("CCTV");
        foreach (GameObject go in gos)
        {
            cameras.Add(go.GetComponentInChildren<Camera>());
        }
    }

    private void FixedUpdate()
    {
        if (AreCamerasActive && UpdateHasBeenDone)
        {
            SetCamerasStatus(false);
        }
    }

    void Update()
    {
        UpdateHasBeenDone = true;
    }

    void CreateRenderTextures()
    {
        int cameraIndex = 0;
        foreach (var meshRenderer in meshRenderers)
        {
            Material screenMat = new Material(screenMaterial);
            RenderTexture renderTexture = new RenderTexture(renderTextureScreen);
            meshRenderer.material.mainTexture = renderTexture;

            if(cameraIndex >= cameras.Count) { return; }

            AssignCameraToScreen(cameraIndex++, renderTexture);
        }
    }

    void AssignCameraToScreen(int index, RenderTexture renderTexture)
    {
        cameras[index].targetTexture = renderTexture;
    }

    void DisableAllCameraTargetTexture()
    {
        foreach (var camera in cameras)
        {
            camera.targetTexture = null;
        }
    }
    public void SetCamerasStatus(bool status)
    {
        AreCamerasActive = status;
        foreach (var camera in cameras)
        {
            camera.enabled = status;
        }
    }
}
