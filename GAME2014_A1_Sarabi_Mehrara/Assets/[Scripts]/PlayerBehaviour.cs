/*
PlayerBehaviour.cs
Author: Mehrara Sarabi 
Student ID: 101247463
Last modified: 2021-10-24
Description: This code encapsulates all player behaviour. It has a move function that moves player tp the last 
touch iput position. enableAttacking will hit and kill enemies if attack button is pressed. applyDamage and Die take
care of when player is hit and when it's dead.
*/
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

    public AudioSource pickupSound;
    public AudioSource hitSound;
    public AudioSource damageSound;


    public Text scoreText;
    public Text healthText;
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
    public int currentScore = 0;
    public GameObject endImage;
    public Text resultText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = ("SCORE: " + currentScore);
        healthText.text = ("HEALTH: " + currentHealth);

        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
        characterScale = transform.localScale;
    }
    public void applyDamage(int damage)
    {
        damageSound.Play();
        currentHealth -= damage;
        healthText.text = ("HEALTH: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        animator.SetTrigger("Dead");
        endImage.SetActive(true);
        resultText.text = "YOU LOST!";
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Time.timeScale = 0;
    }
    public void enableAttacking()
    {
        animator.SetTrigger("AttackTrigger");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            hitSound.Play();
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
        scoreText.text = ("SCORE: " + currentScore);
        healthText.text = ("HEALTH: " + currentHealth);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            //Destroy(collision.gameObject);
            currentScore = currentScore + 50;
            scoreText.text = ("SCORE: " + currentScore);
            pickupSound.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Win")
        {
            endImage.SetActive(true);
            resultText.text = "YOU WON!";
            Time.timeScale = 0;
        }
    }
}
