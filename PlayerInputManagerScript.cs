using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class Item {
    public string tag;
    public GameObject itemObject;
}
[System.Serializable]
public class SpecialEvent {
    public UnityEvent _event;
    public KeyCode _key;
}
public class PlayerInputManagerScript : MonoBehaviour
{
    [Header("Mouse Click Events")]
    public UnityEvent onLeftClick;
    public UnityEvent onLeftUp;
    public UnityEvent onRightClick;
    [Header("Key Events")]
    public SpecialEvent[] KeyEvents;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInputs();
        HandleKeyInputs();
    }
    private void HandleKeyInputs() {
        foreach (SpecialEvent S_event in KeyEvents) {
            if (Input.GetKeyDown(S_event._key)) {
                S_event._event?.Invoke();
            }
        }
    }
    public void ThrowItem() {
        foreach (Item item in items) {
            if (item.itemObject.activeSelf) {
                
            }
        }
    }
    private void HandleMouseInputs()
    {
        // 0 = Left Click
        if (Input.GetMouseButtonDown(0))
        {
            onLeftClick?.Invoke();
        }
        if (Input.GetMouseButtonUp(0)) {
            onLeftUp?.Invoke();
        }

        // 1 = Right Click
        if (Input.GetMouseButtonDown(1))
        {
            onRightClick?.Invoke();
        }
    }
}
