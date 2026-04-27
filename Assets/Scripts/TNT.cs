using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] private GameObject barricade;
    [SerializeField] private LayerMask whatTriggersTNT;
    [SerializeField] private AudioClip explosionSFX;
    [SerializeField] private float explosionVolume = 1f;

    public GameObject explosion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //is the collision within whatTriggersSwitch layerMask
        if ((whatTriggersTNT.value & (1 << collision.gameObject.layer)) > 0)
        {
            print("reached");
            //explosion animation
            Instantiate(explosion, transform.position, Quaternion.identity);

            //play sound effect
            SoundFXManager.instance.PlaySoundFXClip(explosionSFX, transform, explosionVolume);
            //barricade breaking animation

            //destroy barricade
            Destroy(barricade);
            Destroy(gameObject);

        }
    }
}
