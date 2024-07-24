using System.Collections;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public float shootingRange;
    public float fireRate = 1;
    public GameObject bullet;
    public GameObject bulletParent;

    private float nextFireTime;
    private Transform player;
    private Animator anim;
    private SpriteRenderer sprite;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        sprite.flipX = player.position.x > transform.position.x;

        if (distanceToPlayer <= shootingRange && Time.time > nextFireTime)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("atk"))
            {
                StartCoroutine(Attack());
            }
        }
    }
    private IEnumerator Attack()
    {
        anim.SetTrigger("atk");
        nextFireTime = Time.time + fireRate;

        yield return new WaitForSeconds(0.27f);
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetTrigger("idle");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
