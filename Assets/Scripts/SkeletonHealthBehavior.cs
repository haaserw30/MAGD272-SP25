using UnityEngine;

public class SkeletonHealthBehavior : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float currentHealth;

    public void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
        //play SFX

    }

    public void WhenDead()
    {
        throw new System.NotImplementedException();

        //play Death Animation

        //replace sprite with bones

        //wait

        // destroy bones sprite and play revive animation

        //back to beginning
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
}
