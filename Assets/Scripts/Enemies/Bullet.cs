using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private GameObject target;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");

        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Destroy(gameObject, 2);
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
