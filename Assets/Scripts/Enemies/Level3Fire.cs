using UnityEngine;
using System.Collections;

public class Level3Fire : MonoBehaviour
{
    public float shootingRange = 10f;
    public float fireRate = 1f;
    private float nextFireTime;

    public GameObject bulletPrefab;
    public Transform bulletParent;
    private Transform player;

    private Animator anim;
    private SpriteRenderer sprite;

    private float timeBetweenShots = 0.3f;
    private bool isShooting = false;    
    private Vector2 shootingDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer <= shootingRange && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (player.position.x > transform.position.x)
            {
                shootingDirection = Vector2.right;
                sprite.flipX = true;
            }
            else
            {
                shootingDirection = Vector2.left;
                sprite.flipX = false;
            }

            if (!isShooting)
            {
                anim.SetBool("atk", true);
                StartCoroutine(FireBullets());
            }
        }
    }
    IEnumerator FireBullets()
    {
        isShooting = true;

        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletParent.position, Quaternion.identity);
            bullet.GetComponent<FireBall>().SetDirection(shootingDirection);
            yield return new WaitForSeconds(timeBetweenShots);
        }

        isShooting = false;
        anim.SetBool("atk", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }   
}
