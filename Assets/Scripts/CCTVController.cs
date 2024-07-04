using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVController : MonoBehaviour
{
    public GameObject Cameras;
    public RenderTexture renderTextureScreen;
    
    private Camera[] cameraList;
    private int currentCameraIndex;

    public int nextUpdateSeconde = 2;

    void Start()
    {
        cameraList = Cameras.GetComponentsInChildren<Camera>();
        DisableAllCameraTargetTexture();
        SetCameraView(0);
    }

    void Update()
    {
        if (Time.time >= nextUpdateSeconde)
        {
            nextUpdateSeconde = Mathf.FloorToInt(Time.time) + 2;
            SetNextCameravView();
        }
    }

    void SetNextCameravView()
    {
        if (currentCameraIndex == cameraList.Length - 1)
        {
            DisableAllCameraTargetTexture();
            SetCameraView(0);
            currentCameraIndex = 0;
        }
        else
        {
            SetCameraView(++currentCameraIndex);
        }
    }

    void SetCameraView(int index)
    {
        cameraList[index].targetTexture = renderTextureScreen;
        currentCameraIndex = index;
    }

    void DisableAllCameraTargetTexture()
    {
        foreach (var camera in cameraList)
        {
            camera.targetTexture = null;
        }
    }
}
