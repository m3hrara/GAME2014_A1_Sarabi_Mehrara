/*
PickupBehaviour.cs
Author: Mehrara Sarabi 
Student ID: 101247463
Last modified: 2021-10-24
Description: This code plays the pickup sound and destroys the object after.
*/
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
