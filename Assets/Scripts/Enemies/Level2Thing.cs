using System.Collections;
using UnityEngine;

public class Level2Thing : MonoBehaviour
{
    public float speed = 3f;
    public float chasingRange = 5f;
    public float explosiveRange = 3f;
    public LayerMask playerLayer;

    private Transform player;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool isExploding = false;
    private Coroutine explodeCoroutine;
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
       
    }

    void Update()
    {
        if (player != null && !isExploding)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFromPlayer < chasingRange)
            {
                if (distanceFromPlayer < explosiveRange)
                {
                    if (explodeCoroutine == null)
                    {
                        explodeCoroutine = StartCoroutine(ExplodeCoroutine());
                    }
                }
                else
                {
                    if (explodeCoroutine != null)
                    {
                        StopCoroutine(explodeCoroutine);
                        explodeCoroutine = null;
                        anim.SetBool("readyexplode", false);
                    }

                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                    if (player.position.x < transform.position.x)
                    {
                        sprite.flipX = false;
                    }
                    else
                    {
                        sprite.flipX = true;
                    }
                }
            }         
        }
    }

    private IEnumerator ExplodeCoroutine()
    {
        isExploding = true;
        anim.SetBool("readyexplode", true);

        yield return new WaitForSeconds(1f);

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < explosiveRange)
        {
            anim.SetBool("readyexplode", false);
            anim.SetBool("explode", true);
            yield return new WaitForSeconds(0.4f);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosiveRange);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(50);
                    }
                }
            }

            Destroy(gameObject);
        }
        else
        {
            anim.SetBool("readyexplode", false);
            isExploding = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chasingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosiveRange);
    }
}
