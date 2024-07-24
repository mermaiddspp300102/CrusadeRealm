using System.Collections;
using UnityEngine;

public class BossAngry : MonoBehaviour
{
    public Transform shootingMoveTarget;
    public float shootingRange;
    public float shootingFireRate = 1f;
    public float shootingDuration = 3f;
    public GameObject bulletPrefab;
    public Transform shootingBulletParent;

    private Transform player;
    private float nextFireTime;
    private float shootingStartTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public IEnumerator PerformShootingStarAttack()
    {
        bool hasFinishedAttack = false;
        shootingStartTime = Time.time;
        while (Vector2.Distance(transform.position, shootingMoveTarget.position) > 0.1f)
        {
            MoveToTargetPosition(shootingMoveTarget.position);
            yield return null;
        }

        while (!hasFinishedAttack)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if (distanceFromPlayer <= shootingRange && Time.time - shootingStartTime < shootingDuration)
            {
                if (Time.time >= nextFireTime)
                {
                    ShootBulletsInAllDirections();
                    nextFireTime = Time.time + shootingFireRate;
                }
            }
            else
            {
                hasFinishedAttack = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(2f);
    }

    private void MoveToTargetPosition(Vector2 targetPosition)
    {
        float step = shootingRange * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }

    private void ShootBulletsInAllDirections()
    {
        Vector2[] directions = new Vector2[8];
        for (int i = 0; i < 8; i++)
        {
            float angle = Random.Range(0f, 360f);
            directions[i] = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        foreach (Vector2 direction in directions)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingBulletParent.position, Quaternion.identity);
            BossBullet bossBullet = bullet.GetComponent<BossBullet>();
            bossBullet.SetDirection(direction);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
