using UnityEngine;
using System.Collections;

public class EnemiesHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Die();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            TakeDamage(100);
        }
    }
    void Die()
    {
        animator.SetTrigger("dead");
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.13f);
        Destroy(gameObject);
    }
}
