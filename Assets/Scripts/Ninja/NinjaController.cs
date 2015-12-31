using UnityEngine;
using System.Collections;
public class NinjaController : MonoBehaviour {
	[SerializeField]
	float 			movementSpeed = 10f;
    [SerializeField]
    float           jumpForce;

	bool 			isFacingRight = true;
    bool            attack;
    bool            slide;
    bool            jump;
    bool            isGrounded;
    [SerializeField]
    Transform[]     groundPoints;
    [SerializeField]
    LayerMask       ground;
    [SerializeField]
    float           groundRadius;

	// cache
	Rigidbody2D 	mRigidBody;
	Transform		mTransform;
    Animator        anim;
    [SerializeField]
    GameObject      slideCollider;
    [SerializeField]
    GameObject      normalCollider;
	// Use this for initialization
	void Start () {
		mRigidBody  = this.GetComponent<Rigidbody2D>();
		mTransform = this.GetComponent<Transform>();
        anim       = this.GetComponent<Animator>();
        slideCollider.SetActive(false);
	}

	void FixedUpdate(){
        float move = Input.GetAxis("Horizontal");
        isGrounded = this.IsGround();
        HandleMovement(move);
        Flip(move);
        HandleAttack();
        ResetValues();
	}
    void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.jump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.attack = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.slide = true;
        }
    }
    private void HandleAttack()
    {
        if (this.attack && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("NinjaAttack") && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("NinjaJumpAttack"))
        {
            this.anim.SetTrigger("attack");
            mRigidBody.velocity = new Vector2(0, mRigidBody.velocity.y);
        }
 
    }
    void HandleMovement(float move)
    {
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("NinjaSlide") 
            && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("NinjaAttack"))
        {
            mRigidBody.velocity = new Vector2(move * this.movementSpeed, mRigidBody.velocity.y);
        }
        if (isGrounded && jump)
        {
            isGrounded = false;
            mRigidBody.AddForce(new Vector2(0, jumpForce));
        }
        if (this.slide 
                && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("NinjaSlide") 
                && this.anim.GetCurrentAnimatorStateInfo(0).IsName("NinjaRun"))
        {
            this.anim.SetTrigger("slide");
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("NinjaSlide"))
        {
            this.normalCollider.SetActive(false);
            this.slideCollider.SetActive(true);
        }
        else
        {
            this.normalCollider.SetActive(true);
            this.slideCollider.SetActive(false);
        }
        this.anim.SetFloat("velocityVertical", mRigidBody.velocity.y);
        this.anim.SetFloat("speed", Mathf.Abs(move));
    }
    bool IsGround()
    {
        if (this.mRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in this.groundPoints)
            {
                Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(point.position, groundRadius, this.ground);
                for (int i = 0; i < overlapColliders.Length; i++)
                {
                    if (overlapColliders[i].gameObject != this.gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    void Flip(float move)
    {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("NinjaSlide"))
        {
            return;
        }
        if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight))
        {
            this.isFacingRight = !this.isFacingRight;
            Vector3 localScale = this.mTransform.localScale;
            localScale.x *= -1;
            this.mTransform.localScale = localScale;
        }
    }

    void ResetValues()
    {
        this.attack = false;
        this.slide = false;
        this.jump = false;
    }
    
}
