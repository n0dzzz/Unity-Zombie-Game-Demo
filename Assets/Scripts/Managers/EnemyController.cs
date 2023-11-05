using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public float attackRange = 3f; 
    public int attackDamage = 5; 
    public float attackCooldown = 2.0f;
    public bool canAttack = true;
    public int currentHealth;

    private CharacterController characterController;
    private GameObject playerObject;
    private Transform playerTransform; // Reference to the player's transform
    public float speed = 5f; // Movement speed of the GameObject
    private Animator animator; 
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        if (playerObject == null)
            playerObject = GameObject.Find("Player");

        currentHealth = maxHealth;
    }

    void Update()
    {
        Movement();
        AttackPlayer();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerController.Instance.playerMoney += 500.0f;

        // Perform death logic (e.g., play death animation, spawn particle effects, etc.)
        Destroy(gameObject);
        RoundController.Instance.aliveZombies -= 1;
    }

    void Movement()
    {
        playerTransform = playerObject.transform;
        
        if (playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = -5f; // Set the Y component to 0 to keep the object grounded

            // Normalize the direction vector to have a magnitude of 1
            direction.Normalize();

            // Move the GameObject towards the player
            characterController.Move(direction * speed * Time.deltaTime);        
            characterController.transform.rotation = playerTransform.rotation;
        }
    }

    void AttackPlayer()
    {
        // Check if the player transform is assigned
        if (playerTransform != null)
        {
            // Calculate the distance between the player and the GameObject
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Check if the distance is within the specified range
            if (distance <= attackRange)
            {

                if (PlayerController.Instance.playerHp != null)
                {
                    if(canAttack)
                    {
                        PlayerController.Instance.playerHp -= attackDamage;
                        animator.SetTrigger("Attack"); 
                        isAttacking = true;
                        StartCoroutine(WaitForNextAttack());
                    }

                    if (PlayerController.Instance.playerHp <= 0)
                    {
                        Debug.Log("Dead");
                    }
                }
                
            }
        }
    }

    public void AttackAnimationFinished()
    {
        isAttacking = false;
        // Additional logic or transitions can be added here
    }


    private IEnumerator WaitForNextAttack()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;

    }
}