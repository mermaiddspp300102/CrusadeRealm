using UnityEngine;

public class Level2Spider : MonoBehaviour
{
    public float speed = 3f;
    public float lineOfSite = 10f;
    public float attackRange = 1f;
    public float damageAmount = 1f;
    public float damageInterval = 1f; 

    public LayerMask playerLayer;
    private Transform player;
    private Animator anim;
    private SpriteRenderer sprite;

    private float attackTimer;
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        attackTimer = damageInterval;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceFormPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFormPlayer < lineOfSite)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
               
                if (distanceFormPlayer < attackRange)
                {
                    attackTimer -= Time.deltaTime;
                    if(attackTimer <= 0f)
                    {
                        AttackPlayer();
                        attackTimer = damageInterval; 
                    }
                }
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
    private void AttackPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, attackRange, playerLayer);
        if (hit.collider != null)
        {
            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
