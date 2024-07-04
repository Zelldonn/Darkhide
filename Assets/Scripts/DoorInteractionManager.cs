using UnityEngine;
using static DoorStateManager;

public class DoorInteractionManager : MonoBehaviour
{
    public float maxInteractionDistance = 5f;

    private DoorStateManager selectedDoor;

    private GameObject playerUI;

    void Start()
    {
        playerUI = GameObject.Find("Player UI");
    }

    void Update()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, maxInteractionDistance))
            return;

        if (hit.collider.gameObject.tag != "Door")
            return;

        playerUI.GetComponentInChildren<PlayerUIManager>().ShowKeyHint();

        if (Input.GetKeyDown(KeyCode.F))
        {
            selectedDoor = hit.collider.transform.root.gameObject.GetComponentInChildren<DoorStateManager>();
            selectedDoor.ChangeDoorState();
        }
    }
}
