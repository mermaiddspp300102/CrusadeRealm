using UnityEngine;

public class Flame : MonoBehaviour
{
    public float speed;
    public int damage;
    public float maxDistance = 2f;

    private Rigidbody2D rb;
    private Collider2D coll;

    private Vector2 direction;
    private Vector2 startPosition;
    private float traveledDistance = 0f;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Start()
    {
        coll=GetComponent<Collider2D>();    
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        startPosition = transform.position;

        
        Destroy(gameObject, 1.6f);
    }

    void Update()
    {
        traveledDistance = Vector2.Distance(startPosition, transform.position);

        if (traveledDistance >= maxDistance)
        {
            rb.velocity = Vector2.zero;
        }        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
