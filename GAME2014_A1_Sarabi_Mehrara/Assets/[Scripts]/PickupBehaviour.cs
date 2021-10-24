using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    AudioSource pickupSound;
    private void Start()
    {
        pickupSound = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            pickupSound.Play();
            Destroy(this.gameObject);
        }
    }

}
