using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public int damage = 10;

    public LayerMask enemiesLayer;
    public LayerMask bossLayer;
    private void Start()
    {
        anim=GetComponent<Animator>();
    }
    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    private void Attack()
    {
        anim.SetTrigger("Attack");
        int combinedLayerMask = enemiesLayer | bossLayer;
        Collider2D[] hitEntities = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, combinedLayerMask);
        foreach (Collider2D entity in hitEntities)
        {
            EnemiesHealth enemyHealth = entity.GetComponent<EnemiesHealth>();
            Boss bossHealth = entity.GetComponent<Boss>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                continue;
            }
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
                continue;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
