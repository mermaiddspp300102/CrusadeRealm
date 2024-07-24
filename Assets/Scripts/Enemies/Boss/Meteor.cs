using UnityEngine;

public class MeteorBullet : MonoBehaviour
{
    public int damage;
    private void Start()
    {
        Destroy(gameObject,5);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }       
    }
}
