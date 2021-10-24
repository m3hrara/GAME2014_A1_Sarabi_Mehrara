/*
EnemyBehaviour.cs
Author: Mehrara Sarabi 
Student ID: 101247463
Last modified: 2021-10-24
Description: This code encapsulates all enemy behaviour. It has a* pathfinding behaviour and will start following the player
taking the quickest path if player is within a certain distance. It will apply damage on player if they get closer. Takes care of 
animation triggers. Enemy has a randomized gem drop!
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public float direction;
    public Transform startTransform;
    public GameObject gemPrefab;
    private Vector3 startPos;
    public Transform playerTransform;
    public AIPath aiPath;
    public AIDestinationSetter destinationSetter;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 2.5f;
    public LayerMask playerLayer;
    private int frame=240;
    private int maxFrame=240;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isIdle", true);
        startPos = startTransform.position;
        destinationSetter.target = null;
    }
    public void enableAttacking()
    {
        animator.SetTrigger("AttackTrigger");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerBehaviour>().applyDamage(20);
        }
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
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(playerTransform.position, transform.position)) <= 1.5f)
        {
            if (frame == maxFrame)
            {
                Debug.Log("in range");
                enableAttacking();
                frame = 0;
            }
            else
                frame++;

        }

        SetLocalScale();
        if ((transform.position.x - startPos.x < 0.01f || transform.position.x - startPos.x < -0.01f) &&
            (transform.position.y - startPos.y < 0.01f || transform.position.y - startPos.y < -0.01f))

        {
            animator.SetBool("isIdle", true);
        }
        else
            animator.SetBool("isIdle", false);
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
        player.GetComponent<PlayerBehaviour>().currentScore += 50;
        player.GetComponent<PlayerBehaviour>().scoreText.text = ("SCORE: " + player.GetComponent<PlayerBehaviour>().currentScore);
        Random.InitState(System.DateTime.Now.Millisecond);

        int rand = Random.Range(1, 10);
        if (rand < 6)
        {
            Instantiate(gemPrefab, transform.position, Quaternion.identity);
        }


        animator.SetBool("isDead", true);
        this.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;


    }
}
