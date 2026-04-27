using System;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed = 15f;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;
    [SerializeField] private AudioClip explosionImpact;
    [SerializeField] private float explosionVolume = 1f;
    [SerializeField] private int bulletDamage = 1;

    private Rigidbody2D rb;
    public GameObject explosion;
  
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDestroyTime();
        SetStraightVelocity();
    }

    private void FixedUpdate()
    {
        transform.right = rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //is the collision within whatDestroysBullet layerMask
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            //explosion animation
            Instantiate(explosion, transform.position, Quaternion.identity);

            //play sound effect
            SoundFXManager.instance.PlaySoundFXClip(explosionImpact, transform, explosionVolume);

            //damage enemy
            IDamagable iDamagable = collision.gameObject.GetComponent<IDamagable>();
            if (iDamagable != null)
            {
                iDamagable.TakeDamage(bulletDamage);
            }

            //destroy bullet
            Destroy(gameObject);
            
        }
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * normalBulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
}
