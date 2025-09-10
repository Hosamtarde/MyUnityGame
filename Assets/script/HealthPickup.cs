using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 100, 0);

    private AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
                
                if (pickupSource && pickupSource.clip != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip, transform.position, pickupSource.volume);
                }

                
                Destroy(gameObject);
            }
            else
            {
                
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
