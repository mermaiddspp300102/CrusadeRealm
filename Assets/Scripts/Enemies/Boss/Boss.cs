using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healBar;

    private BossMeleeAttack meleeAttack;
    private BossAngry shootingStarAttack;
    private BossBreathAttack breathAttack;
    private BossMeteor meteorAttack;
    private BossDash dashAttack;

    private Transform player;
    private bool usedShootingStarOnce = false;
    private bool usedShootingStarTwice = false;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        meleeAttack = GetComponent<BossMeleeAttack>();
        shootingStarAttack = GetComponent<BossAngry>();
        breathAttack = GetComponent<BossBreathAttack>();
        meteorAttack = GetComponent<BossMeteor>();
        dashAttack = GetComponent<BossDash>();
        StartCoroutine(AttackCycle());
    }

    private void Update()
    {
        healBar.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0, 1);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator AttackCycle()
    {
        while (true)
        {
            if (currentHealth > maxHealth / 2)
            {
                yield return StartCoroutine(meleeAttack.PerformMeleeAttack());
                yield return StartCoroutine(dashAttack.PerformDashAttack());
            }
            else
            {
                if (!usedShootingStarOnce)
                {
                    yield return StartCoroutine(shootingStarAttack.PerformShootingStarAttack());
                    usedShootingStarOnce = true;
                }
               
                yield return StartCoroutine(meteorAttack.PerformMeteorAttack());
                yield return StartCoroutine(breathAttack.PerformBreathAttack());
                yield return StartCoroutine(dashAttack.PerformDashAttack());

                if (currentHealth <= maxHealth * 0.25f && !usedShootingStarTwice)
                {
                    yield return StartCoroutine(shootingStarAttack.PerformShootingStarAttack());
                    usedShootingStarTwice = true;
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
