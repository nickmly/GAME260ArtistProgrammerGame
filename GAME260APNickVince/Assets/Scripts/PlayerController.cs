using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float turnSpeed = 10.0f;
    [SerializeField]
    private float jumpHeight = 20.0f;
    [SerializeField]
    private Animation attackAnimation;

    private float attackDelay;
    private float groundCheckHeight = 1f;
    private Rigidbody rb;
    private Animator animator;
    private bool attacking = false;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        attackDelay = 0.867f;
	}
	
	void Update ()
    {
        HandleActions();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void StartAttack()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("attack");
            Invoke("FinishAttack", attackDelay);
        }        
    }

    void FinishAttack()
    {
        attacking = false;
    }

    void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            StartAttack();
        }
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        Vector3 rotEuler = transform.rotation.eulerAngles;
        rotEuler.y += x * turnSpeed;
        Quaternion rot = transform.rotation;
        rot.eulerAngles = rotEuler;

        transform.rotation = rot;

        Vector3 currentVelocity = new Vector3(rb.velocity.x, rb.velocity.y, z) * moveSpeed;
        rb.position += transform.TransformDirection(currentVelocity * Time.deltaTime);
        HandleAnimator(z);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0));
        }
    }

    void HandleAnimator(float z)
    {
        animator.SetFloat("moving", Mathf.Abs(z));
        animator.SetBool("inAir", !IsGrounded());
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckHeight))
        {
            return true;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector3.down * groundCheckHeight);
    }
}
