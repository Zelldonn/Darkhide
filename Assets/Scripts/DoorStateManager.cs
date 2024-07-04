using UnityEngine;

public class DoorStateManager : MonoBehaviour
{
    public enum DoorState { OPEN, CLOSED };
    public DoorState doorState = DoorState.CLOSED;
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        setDoorState(doorState);

    }

    private void setDoorState(DoorState state)
    {
        if(state == DoorState.OPEN)
            openDoor();
        else
            closeDoor();
    }

    public void ChangeDoorState(bool playSound=true)
    {
        if(playSound)
            GetComponentInChildren<AudioSource>().Play();

        if (doorState == DoorState.OPEN)
            closeDoor();
        else
            openDoor();
    }

    private void openDoor()
    {
        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");
        doorState = DoorState.OPEN;
    }

    private void closeDoor()
    {
        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");
        doorState = DoorState.CLOSED;
    }

    void Update()
    {
        
    }
}
