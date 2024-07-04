using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class CollectableManager : MonoBehaviour
{
    public int itemsCollected { get; private set; }
    public int totalItems { get; private set; }

    GameObject[] Items;
    CommandAndControlManager commandAndControlManager;

    public UnityEvent LaserCollect, StopTimer;


    void Start()
    {
        Items = GameObject.FindGameObjectsWithTag("Collectable");

        itemsCollected = 0;
        totalItems = Items.Length;

        commandAndControlManager = GameObject.Find("Managers").GetComponent<CommandAndControlManager>();
        LaserCollect.AddListener(GameObject.Find("Managers").GetComponent<HUDManager>().UpdateCounter);
        StopTimer.AddListener(GameObject.Find("Managers").GetComponent<HUDManager>().StopTimer);

        DisableAllItems();
        DisplayNextItem();
        LaserCollect?.Invoke();
    }

    void Update()
    {

    }
    void DisableAllItems()
    {
        foreach (var item in Items)
        {
            item.SetActive(false);
        }
    }
    void DisplayNextItem()
    {
        if(Items.Length < 1)  return; 

        Items[itemsCollected].SetActive(true);
    }

    void print()
    {
        Debug.Log("Items collected: " + itemsCollected); 
        Debug.Log("Collectable items present in the map : " + totalItems);
    }
    public void incrementItem()
    {
        itemsCollected++;
        if (itemsCollected != totalItems)
            DisplayNextItem();
        else
        {
            StopTimer?.Invoke();
        }
        commandAndControlManager.SetCamerasStatus(true);
        LaserCollect?.Invoke();
    }

}
