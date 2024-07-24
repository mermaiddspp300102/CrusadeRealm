using System.Collections;
using UnityEngine;

public class BossBreathAttack : MonoBehaviour
{
    private Animator anim;
    public Transform breathAttackPoint;
    public float breathAttackRange = 0.5f;
    public float breathDetectionRange = 3f;
    public int breathDamage = 10;
    public float breathInitialDelay = 0.25f;
    public float breathAttackDuration = 2f;
    public float breathDamageInterval = 0.5f;
    public float breathMoveSpeed = 2f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    public IEnumerator PerformBreathAttack()
    {
        bool hasFinishedAttack = false;
        float elapsedTime = 0f;

        while (!hasFinishedAttack)
        {
            bool playerInRange = Vector2.Distance(transform.position, player.position) <= breathDetectionRange;

            if (playerInRange)
            {
                anim.SetTrigger("breath");
                yield return new WaitForSeconds(breathInitialDelay);

                while (elapsedTime < breathAttackDuration)
                {
                    InflictDamage();
                    yield return new WaitForSeconds(breathDamageInterval);
                    elapsedTime += breathDamageInterval;
                }

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
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(breathAttackPoint.position, breathAttackRange, LayerMask.GetMask("Player"));
        foreach (Collider2D hitPlayer in hitPlayers)
        {
            PlayerHealth playerHealth = hitPlayer.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(breathDamage);
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, breathMoveSpeed * Time.deltaTime);
        
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        
    }

    private void OnDrawGizmosSelected()
    {
        if (breathAttackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(breathAttackPoint.position, breathAttackRange);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, breathDetectionRange);
        }
    }
}
