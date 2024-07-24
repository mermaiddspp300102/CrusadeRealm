using UnityEngine;
using System.Collections;

public class Level3Burn : MonoBehaviour
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
    private Vector2 shootingDirection = Vector2.up;
    public Vector2 startPosition;
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

            

            if (!isShooting)
            {               
                StartCoroutine(FireBullets());
            }
        }
    }
    IEnumerator FireBullets()
    {
        isShooting = true;
        anim.SetBool("burn", true);        

        GameObject bullet = Instantiate(bulletPrefab, bulletParent.position, Quaternion.identity);
        bullet.GetComponent<Flame>().SetDirection(shootingDirection);
        yield return new WaitForSeconds(1.4f);
        yield return new WaitForSeconds(timeBetweenShots);
        

        isShooting = false;
        anim.SetBool("burn", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
