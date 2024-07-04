using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class Ipad : NetworkBehaviour
{
    public RenderTexture renderTextureScreen;

    private Camera[] cameras;
    private int currentCameraIndex = 0;
    private bool _isIpadOpen = false;

    void Start()
    {
  
        cameras = GameObject.Find("CameraList").GetComponentsInChildren<Camera>();
        DisableAllCameraTargetTexture();
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCameraMode();
        }
        if (!_isIpadOpen) return;

        if (Input.GetKeyDown(KeyCode.Mouse0)){
            ChangeCameraView(-1);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeCameraView(1);
        }
    }
    private void SwitchCameraMode()
    {
        if (!_isIpadOpen)
        {
            SetCameraView(currentCameraIndex);
        }
        else
        {
            DisableAllCameraTargetTexture(); 
        }

        _isIpadOpen = !_isIpadOpen;
    }

    void ChangeCameraView(int direction)
    {
        DisableAllCameraTargetTexture();
        if (currentCameraIndex + direction == cameras.Length) {
            SetCameraView(0);
            currentCameraIndex = 0;
        }
        else if (currentCameraIndex + direction == -1)
        {
            currentCameraIndex = cameras.Length - 1;
            SetCameraView(cameras.Length - 1);
        }
        else
        {
            currentCameraIndex += direction;
            SetCameraView(currentCameraIndex);
        }
    }
    void DisableAllCameraTargetTexture()
    {
        foreach (var camera in cameras)
        {
            camera.depth = 0;
            camera.targetTexture = null;
            camera.enabled = false;
        }
    }
    void SetCameraView(int index)
    {
        cameras[index].enabled = true;
        cameras[index].targetTexture = renderTextureScreen;
        cameras[index].depth = 2;
        currentCameraIndex = index;
    }
}
