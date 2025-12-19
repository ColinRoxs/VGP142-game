using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private int damage = 50;
    [SerializeField] private LayerMask targetLayers;

    

    private void OnTriggerEnter(Collider other)
    {
        // Only affect objects on the target layer(s)
        if ((targetLayers.value & (1 << other.gameObject.layer)) == 0)
            return;

        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}

