using UnityEngine;

public class Level3Ghost : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1;
    private float nextFireTime;

    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;

    private Animator anim;
    private SpriteRenderer sprite;

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
        if (player != null)
        {
            float distanceFormPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFormPlayer < lineOfSite && distanceFormPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                anim.SetBool("run", true);
                sprite.flipX = player.position.x < transform.position.x;
            }
            else if (distanceFormPlayer <= shootingRange && nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                anim.SetBool("run", false);
            }
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
