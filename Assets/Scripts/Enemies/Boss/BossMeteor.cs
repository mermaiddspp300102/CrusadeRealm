using System.Collections;
using UnityEngine;

public class BossMeteor : MonoBehaviour
{
    public float meteorMoveSpeed = 5f;
    public Transform meteorObject1;
    public Transform meteorObject2;
    public float meteorFireRate = 1f;
    public GameObject bossMeteorPrefab;
    public Transform meteorBulletParent;

    private bool isDroppingMeteors = false;

    public IEnumerator PerformMeteorAttack()
    {
        bool isMovingToObject1 = true;
        bool hasFinishedAttack = false;

        while (!hasFinishedAttack)
        {
            if (isMovingToObject1)
            {
                MoveToTargetPosition(meteorObject1.position);
                if (Vector2.Distance(transform.position, meteorObject1.position) < 0.1f)
                {
                    isMovingToObject1 = false;
                    isDroppingMeteors = true;
                    StartCoroutine(DropMeteors());
                }
            }
            else
            {
                MoveToTargetPosition(meteorObject2.position);
                if (Vector2.Distance(transform.position, meteorObject2.position) < 0.1f)
                {
                    isDroppingMeteors = false;
                    hasFinishedAttack = true;
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(2f);
    }

    private IEnumerator DropMeteors()
    {
        while (isDroppingMeteors)
        {
            GameObject meteor = Instantiate(bossMeteorPrefab, meteorBulletParent.position, Quaternion.identity);
            yield return new WaitForSeconds(1f / meteorFireRate);
        }
    }

    private void MoveToTargetPosition(Vector2 targetPosition)
    {
        float step = meteorMoveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }

    private void OnDrawGizmosSelected()
    {
        if (meteorObject1 != null && meteorObject2 != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(meteorObject1.position, 0.5f);
            Gizmos.DrawWireSphere(meteorObject2.position, 0.5f);
        }
    }
}
