using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text counter, timer;

    bool b_IsTimerRunning = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!b_IsTimerRunning) return;
        UpdateTimer();
    }

    private void setCounter(int current, int total)
    {
        counter.text = "Objects found : " + current.ToString() + "/" + total;
    }
    private void UpdateTimer()
    {
        float currentTime = Time.time;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        b_IsTimerRunning=false;
    }

    public void UpdateCounter()
    {
        int totalItems = GameObject.Find("Managers").GetComponent<CollectableManager>().totalItems;
        int current = GameObject.Find("Managers").GetComponent<CollectableManager>().itemsCollected;
        setCounter(current, totalItems);
    }
}
