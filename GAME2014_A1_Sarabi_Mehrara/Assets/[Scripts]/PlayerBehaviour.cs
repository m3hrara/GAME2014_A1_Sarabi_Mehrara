using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Speed")]
    public float playerSpeed;
    public float maxSpeed;
    public float lerpTime;

    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;
    private Vector3 characterScale;
    private Vector2 m_Direction;
    private int touchID;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    private int maxHealth = 100;
    private int currentHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
        characterScale = transform.localScale;
    }
    //public void applyDamage(int damage)
    //{
    //    currentHealth -= damage;
    //    if(currentHealth<=0)
    //    {
    //        Die();
    //    }
    //}
    //private void Die()
    //{
    //    Debug.Log(" asd Die yo");
    //    animator.SetBool("isDead", true);
    //    this.enabled = false;
    //    GetComponent<Collider2D>().enabled = false;
    //}
    public void enableAttacking()
    {
        animator.SetTrigger("AttackTrigger");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit");
            enemy.GetComponent<EnemyBehaviour>().Die();
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
        _Move();
    }
    private void _Move()
    {
        // touch input support
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (worldTouch.x > transform.position.x)
                {
                    characterScale.x = 1.0f;
                }

                if (worldTouch.x < transform.position.x)
                {
                    characterScale.x = -1.0f;
                }
            }

            m_touchesEnded = worldTouch;

        }
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (m_touchesEnded.x != 0.0f || m_touchesEnded.y != 0.0f)
            {
                m_Direction = new Vector2(m_touchesEnded.x - transform.position.x, m_touchesEnded.y - transform.position.y);

                Vector2 newVelocity = m_rigidBody.velocity + m_Direction * playerSpeed;
                m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
                m_rigidBody.velocity *= 0.99f;
            }
        }
        if(m_rigidBody.velocity.magnitude <= 0.5)
        {
            m_rigidBody.velocity = new Vector2(0.0f, 0.0f);
        }
        transform.localScale = characterScale;
        animator.SetFloat("Speed", m_rigidBody.velocity.magnitude);
    }

}
