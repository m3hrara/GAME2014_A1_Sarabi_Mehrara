using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public float direction;
    public Transform startTransform;
    private Vector3 startPos;
    public Transform playerTransform;
    public AIPath aiPath;
    public AIDestinationSetter destinationSetter;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 2.5f;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isIdle", true);
        startPos = startTransform.position;
        destinationSetter.target = null;
    }
    //public void enableAttacking()
    //{
    //    animator.SetTrigger("AttackTrigger");
    //    Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
    //    foreach(Collider2D player in hitPlayer)
    //    {
    //        Debug.Log("asd damage");
    //        player.GetComponent<PlayerBehaviour>().applyDamage(20);
    //    }
    //}
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
    //private void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //        return;
    //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}
    // Update is called once per frame
    void Update()
    {
        //if ((Vector3.Distance(playerTransform.position, transform.position)) <= 2f)
        //{
        //    Debug.Log("in range");
        //    enableAttacking();
        //}

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
    //    if ((transform.position.x - playerTransform.position.x < 0.05f || transform.position.x - playerTransform.position.x < -0.05f) &&
    //(transform.position.y - playerTransform.position.y < 0.05f || transform.position.y - playerTransform.position.y < -0.05f))

    //    {
    //        Debug.Log("asd in range");
    //        enableAttacking();
    //    }

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
        animator.SetBool("isDead", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
