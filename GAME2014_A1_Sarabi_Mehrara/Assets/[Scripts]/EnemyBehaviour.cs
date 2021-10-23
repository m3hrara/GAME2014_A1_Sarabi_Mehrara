using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public float direction;
    public Transform startTransform;
    public Transform playerTransform;
    public AIPath aiPath;
    public AIDestinationSetter destinationSetter;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        destinationSetter.target = startTransform;
    }
    private void SetLocalScale()
    {
        if(aiPath.desiredVelocity.x>0)
        {
            transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        }
        else if (aiPath.desiredVelocity.x < 0)
        {
            transform.localScale = new Vector3(-0.25f, 0.25f, 1f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        SetLocalScale();
    }
    private void followPlayer()
    {
        destinationSetter.target = playerTransform;
    }
    private void patrol()
    {
        destinationSetter.target = startTransform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            followPlayer();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            patrol();
        }
    }
    public void Die()
    {
        Debug.Log("Deadanim");
        animator.SetBool("isDead", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
