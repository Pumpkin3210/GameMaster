using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Radius & Tag Settings")]
    [Tooltip("The radius around this GameObject to check.")]
    public float checkRadius = 3f;
    
    public Item[] items;

    void Update()
    {
        
    }
    public Collider2D[] getColliders(float radius) {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position,radius);
    }
    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("Found Colliders");
            if (col.gameObject == this.gameObject || col.gameObject == null) return;
            foreach (Item item in items)
            {
                if (col.gameObject.tag == item.tag)
                {
                    Debug.Log("Found Item With Tag");
                    col.gameObject.SetActive(false);

                    if (item.itemObject != null)
                    {
                        item.itemObject.SetActive(true);
                    }
                }
                break; 
            }
    }
}
