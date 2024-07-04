using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject keyHint, NetworkManager;

    private float lastTimeInteraction;

    private float keyHintStayDuration = 0.1f;
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTimeInteraction + keyHintStayDuration > Time.time)
            HideKeyHint();
    }

    public void ShowKeyHint()
    {
        keyHint.SetActive(true);
        lastTimeInteraction = Time.time;
    }
    public void HideKeyHint()
    {
        keyHint.SetActive(false);
    }
}
