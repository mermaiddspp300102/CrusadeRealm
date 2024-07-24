using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public int damage;

    private Rigidbody2D rb;
    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 180));
        Destroy(gameObject, 3);
    }


    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rb.velocity.normalized, speed * Time.deltaTime);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            hit.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }   
}
