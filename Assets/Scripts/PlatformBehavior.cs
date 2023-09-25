using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float bounciness; // Modify as needed
    public float stickiness; // Modify as needed
    
    private Rigidbody2D rb;
    private Collider2D coll;
    public float speed = 1.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    public void ApplyElementEffect(string elementName)
{
    Debug.Log(elementName);

    switch (elementName)
    {
        case "cloud(Clone)":
            // Make platform bouncy
            coll.sharedMaterial = new PhysicsMaterial2D { bounciness = bounciness };
            break;
        case "lava(Clone)":
            // Make platform non-interactive
            coll.enabled = false;
            break;
        case "steam(Clone)":
            // Make platform move up
            transform.position += new Vector3(0,speed) * speed * Time.deltaTime;
            break;
        case "mud(Clone)":
            // Make platform sticky
            coll.sharedMaterial = new PhysicsMaterial2D { friction = stickiness };
            break;
        case "water":
            // Example: Reduce platform speed
            // Replace with actual effect logic for water
            rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y);
            break;
        case "fire":
            // Example: Make platform emit light
            // Replace with actual effect logic for fire
            gameObject.AddComponent<Light>().range = 5f;
            break;
        case "earth":
            // Example: Increase platform mass
            // Replace with actual effect logic for earth
            rb.mass *= 2f;
            break;
        case "air":
            // Example: Make platform levitate
            // Replace with actual effect logic for air
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Abs(rb.velocity.y) + 2f);
            break;
        case "lightning(Clone)":
            // Example: Electrify platform
            // Replace with actual effect logic for lightning
            // Could be some visual effect or status effect
            break;
        case "mirror(Clone)":
            // Example: Reflect projectiles
            // Replace with actual effect logic for mirror
            // Could involve implementing a reflection mechanic
            break;
        default:
            Debug.Log($"No special properties defined for {elementName}");
            break;
    }
}

}
