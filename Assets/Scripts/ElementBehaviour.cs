using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementBehaviour : MonoBehaviour
{
    [SerializeField]
    private PhysicsMaterial2D bouncyMaterial; // Assign your 2D BouncyMaterial here in the inspector.
    [SerializeField]
    private LayerMask nonInteractiveLayer; // Assign a non-interactive layer here
    [SerializeField]
    private float moveUpSpeed = 1f; // Speed at which the object moves up

    private bool isDragging = false;
    private GameObject draggedObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hitInfo.collider != null)
            {
                draggedObject = hitInfo.collider.gameObject;
                isDragging = true;
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
            
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject == gameObject) // This object is the target
                {
                    
                    ApplySpecialProperties(draggedObject);
                    // draggedObject.SetActive(false);
                }
            }

            isDragging = false;
            draggedObject = null;
        }
    }

    private void ApplySpecialProperties(GameObject obj)
    {
        string elem = obj.name;
        Debug.Log(elem);
        if (elem == "mud"){
        // Make Sticky
        obj.transform.parent = transform;
        }
        
        if (elem == "steam"){
        // Move Up
        obj.AddComponent<Mover>().speed = moveUpSpeed;
        }
        
        // Non-Interactive
        if (elem == "lava"){
        obj.layer = nonInteractiveLayer;
        }
        
        if (elem == "mirror"){
        // Reflective & Bouncy
        Collider2D col = obj.GetComponent<Collider2D>();
        obj.AddComponent<Reflector>();
        }

        if (elem == "cloud"){
            Collider2D col = obj.GetComponent<Collider2D>();
            col.sharedMaterial = bouncyMaterial;
        }
    
    }
}

public class Mover : MonoBehaviour
{
    public float speed = 1f;
    
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}

public class Reflector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = -rb.velocity; // Reflect the velocity
        }
    }
}
