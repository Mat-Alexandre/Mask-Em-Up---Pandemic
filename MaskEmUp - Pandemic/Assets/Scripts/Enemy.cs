using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform walkPoint;
    public Transform playerTransform;
    public GameObject maskSprite;
    public GameObject particleSystem;
    public Animator enemyAnimator;
    public float minDistance = 5f;
    public float maxSpeed = 2f;
    public float timeToWalk = 2f;
    public int[] bounds;

    private float currentSpeed;
    private bool isProtected = false;
    private bool isInfected = false;
    private Transform planeTransform;
    private float waitTime;
    private Vector3 wkPosition;
    private bool isFacingRight = true;
    private bool beingChasing = false;

    
    void Start()
    {
        // Getting the plane bounds
        planeTransform = GameObject.Find("Main Ground").GetComponent<Transform>();
        // 0,1: horizontal;   2,3: vertical     each scale = 10x

        // Set walkPoint to a new location
        walkPoint.position = new Vector3(Random.Range(bounds[0], bounds[1]), 0, Random.Range(bounds[2], bounds[3]));
        wkPosition = walkPoint.position;
        waitTime = timeToWalk;
    }

    void Update()
    {
        // Do while is not protected (not defeated)
        if(!isProtected)
        {
            float playerDistance = Vector3.Distance(transform.position, playerTransform.position);
            
            if( beingChasing )
            {
                enemyAnimator.SetBool("Running", true);
                Run();

                if( playerDistance >= 2 * minDistance )
                    beingChasing = false;
            }
            if( playerDistance >= minDistance && !beingChasing)
            {
                enemyAnimator.SetBool("Walking", true);
                Move();
            }
            if( playerDistance < minDistance && !beingChasing)
            {
                beingChasing = true;
                enemyAnimator.SetBool("Running", true);
                Run();
            }
            // enemyAnimator.SetBool("Walking", true);
            // Move();
            
        }
        else{
            enemyAnimator.SetBool("Running", false);
            enemyAnimator.SetBool("Walking", false);
            this.enabled = false;
        }
    }

    void Move()
    {
        currentSpeed = maxSpeed * 0.75f;
        enemyAnimator.SetBool("Running", false);

        float horizontal = wkPosition.x - transform.position.x;
        if( horizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if( horizontal < 0 && isFacingRight)
        {
            Flip();
        }
        
        // Go to walkpoint position
        transform.position = Vector3.MoveTowards(transform.position, wkPosition, currentSpeed * Time.deltaTime);
        float wpDistance = Vector3.Distance(transform.position, wkPosition);
        
        // If enemy is nearby the walkPoint calculate new position
        if( wpDistance < 0.01f )
        {
            if( waitTime <= 0 )
            {
                walkPoint.position = new Vector3(Random.Range(bounds[0], bounds[1]), 0, Random.Range(bounds[2], bounds[3]));
                wkPosition = walkPoint.position;
                waitTime = timeToWalk;
            }else{
                waitTime -= Time.deltaTime;
                // Play Idle animation
                enemyAnimator.SetBool("Walking", false);
            }
        }
    }

    void Run()
    {
        currentSpeed = maxSpeed;
        float horizontal = transform.position.x - playerTransform.position.x;
        if( horizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if( horizontal < 0 && isFacingRight)
        {
            Flip();
        }


        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, -currentSpeed * Time.deltaTime);
    }

    public void SetIsProtected(bool value)
    {
        this.isProtected = value;
        maskSprite.SetActive(value);
    }

    public bool GetIsProtected()
    {
        return this.isProtected;
    }

    public void SetIsInfected(bool value)
    {
        this.isInfected = value;
    }

    public void Protect()
    {
        SetIsProtected(true);
    }

    public void Particles()
    {
        particleSystem.SetActive(true);
    }
    public bool GetIsInfected()
    {
        return this.isProtected;
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
