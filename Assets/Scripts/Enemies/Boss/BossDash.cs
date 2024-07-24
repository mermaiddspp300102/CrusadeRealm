using System.Collections;
using UnityEngine;

public class BossDash : MonoBehaviour
{
    public float dashSpeed = 5f;
    public float dashDistanceMultiplier = 4f;
    public Transform player;
    public int damage = 10;
    public float attackRange = 10f;
    public LayerMask playerLayer;
    public LayerMask wallLayer;
    private Animator anim;
    private bool isDashing = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator PerformDashAttack()
    {
        yield return new WaitForSeconds(1f);

        Vector2 dashDirection = (player.position.x - transform.position.x > 0) ? Vector2.right : Vector2.left;
        Vector2 dashTarget = new Vector2(transform.position.x + dashDirection.x * dashDistanceMultiplier, transform.position.y);

        isDashing = true;
        while (Mathf.Abs(transform.position.x - dashTarget.x) > 0.1f)
        {
            MoveToTargetPosition(dashTarget, dashDirection);
            anim.SetTrigger("dash");

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, attackRange, playerLayer);
            if (hit.collider != null)
            {
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }

            RaycastHit2D wallHit = Physics2D.Raycast(transform.position, dashDirection, dashSpeed * Time.deltaTime, wallLayer);
            if (wallHit.collider != null)
            {
                transform.position = new Vector2(dashTarget.x, transform.position.y);
                break;
            }

            yield return null;
        }

        isDashing = false;
        yield return new WaitForSeconds(2f);
    }

    private void MoveToTargetPosition(Vector2 targetPosition, Vector2 dashDirection)
    {
        float step = dashSpeed * Time.deltaTime;
        transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, targetPosition.x, step), transform.position.y);

        anim.SetFloat("DashDirection", dashDirection.x);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
