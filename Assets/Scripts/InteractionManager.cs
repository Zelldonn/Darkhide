
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public float maxInteractionDistance = 2f;
    private GameObject playerUI;

    void Start()
    {
        playerUI = GameObject.Find("Player UI");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, maxInteractionDistance))
            return;

        if (hit.collider.gameObject.tag != "Interactable")
            return;

        playerUI.GetComponentInChildren<PlayerUIManager>().ShowKeyHint();
    }
}
