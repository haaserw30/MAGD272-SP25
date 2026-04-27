using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{

    [SerializeField] private float maxHealth = 3f;
    private float currentHealth;
    public void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void WhenDead()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
