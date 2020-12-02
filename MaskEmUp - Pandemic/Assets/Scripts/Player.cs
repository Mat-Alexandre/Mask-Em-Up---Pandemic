using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 2f;
    public Transform hitCheck;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Attack"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerRb.velocity = new Vector3(horizontal * playerSpeed, playerRb.velocity.y, vertical * playerSpeed);

        // Change the animation
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            playerAnim.SetBool("Walking", true);
            
        }else{
            playerAnim.SetBool("Walking", false);
        }

        if( horizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if( horizontal < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Attack()
    {
        // Play attack animation
        playerAnim.SetTrigger("Attacking");

        // Detect enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(hitCheck.position, attackRange, enemyLayer);

        // Put mask
        foreach(Collider enemy in hitEnemies)
        {
            // enemy put mask
            enemy.GetComponent<Enemy>().Invoke("Protect", 0.5f);
        }
    }
}
