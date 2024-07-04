using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserTrigger : MonoBehaviour 
{
    bool hasBeenActivated = false;
    AudioSource audioSource;
    MeshRenderer meshRenderer;

    public UnityEvent LaserCollect;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        LaserCollect.AddListener(GameObject.Find("Managers").GetComponent<CollectableManager>().incrementItem);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (hasBeenActivated) { return; }

        audioSource.Play();
        meshRenderer.enabled = false;
        hasBeenActivated = true;
        LaserCollect.Invoke();
    }
}
