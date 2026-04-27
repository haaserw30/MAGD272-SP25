using UnityEngine;

public class CrystalSwitch : MonoBehaviour
{
    [SerializeField] private GameObject crystal;
    [SerializeField] private GameObject newSprite;
    [SerializeField] private LayerMask whatTriggersUnlock;
    [SerializeField] private AudioClip shatterSFX;
    [SerializeField] private float shatterVolume = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //is the collision within whatTriggersSwitch layerMask
        if ((whatTriggersUnlock.value & (1 << collision.gameObject.layer)) > 0)
        {
            //animation

            //play sound effect
            SoundFXManager.instance.PlaySoundFXClip(shatterSFX, transform, shatterVolume);
            //crystal breaking animation

            //destroy crystal and replace switch
            Instantiate(newSprite, transform.position, Quaternion.identity);
            Destroy(crystal);
            Destroy(gameObject);

        }
    }
}
