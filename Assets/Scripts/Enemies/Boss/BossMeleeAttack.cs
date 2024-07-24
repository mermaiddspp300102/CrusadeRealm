using System.Collections;
using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    private Animator anim;
    public Transform meleeAttackPoint;
    public float meleeAttackSize = 0.5f;
    public float meleeAttackRange = 3f;

    public LayerMask playerLayer;
    public int meleeDamage = 10;
    public float meleeAttackDelay = 0.5f;
    public float meleeMoveSpeed = 2f;

    private Transform player;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    public IEnumerator PerformMeleeAttack()
    {
        bool hasFinishedAttack = false;
        while (!hasFinishedAttack)
        {
            bool playerInRange = Vector2.Distance(transform.position, player.position) <= meleeAttackRange;

            if (playerInRange)
            {
                anim.SetTrigger("attack");
                yield return new WaitForSeconds(meleeAttackDelay);
                InflictDamage();
                hasFinishedAttack = true;
            }
            else
            {
                MoveTowardsPlayer();
            }

            yield return null;
        }

        yield return new WaitForSeconds(2f);
    }

    private void InflictDamage()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackSize, playerLayer);
        foreach (Collider2D hitPlayer in hitPlayers)
        {
            PlayerHealth playerHealth = hitPlayer.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(meleeDamage);
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, meleeMoveSpeed * Time.deltaTime);
        if (!isAttacking)
        {           
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (meleeAttackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(meleeAttackPoint.position, new Vector3(meleeAttackSize, meleeAttackSize*4, 1));
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(meleeAttackRange, meleeAttackRange, 1));
        }
    }
}
