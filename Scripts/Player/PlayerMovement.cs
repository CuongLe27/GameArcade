using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask ladderLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;
    private float verticalInput;
    private bool isRightButton;
    private bool isLeftButton;
    private bool isClimpButton;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput>0.01f)
        {
            transform.localScale = Vector3.one;
        }else if(horizontalInput<-0.01f)
        {
            transform.localScale =new Vector3(-1,1,1);
        }       
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());
        if (wallJumpCoolDown > 0.2f)
        {
            if (isRightButton == true)
            {
                //body.velocity = new Vector2(1 * speed * Time.deltaTime, body.velocity.y);
                transform.Translate(Vector2.right * 0.4f * speed * Time.deltaTime);
                transform.localScale = Vector3.one;
                anim.SetBool("Run", true);
            }
            if (isLeftButton == true)
            {
                //body.velocity = new Vector2(Vector2.right * -1 * speed, body.velocity.y);
                transform.Translate(Vector2.right * -0.4f * speed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                anim.SetBool("Run", true);
            }
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                GetJump();
            }
        }
        else
            wallJumpCoolDown += Time.deltaTime;
        GetClimp();
        getClimpForMobile();
    }
    public void GetJump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput != 0 || isLeftButton == true || isRightButton == true)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            wallJumpCoolDown = 0;
        }
    }
    private void GetClimp()
    { 
        //Check Is trigger in box collider 2d to go throught
        verticalInput = Input.GetAxis("Vertical");
        if (isLadder() && Input.GetAxis("Vertical") != 0)
        {
            body.velocity = new Vector2(body.velocity.x, verticalInput * speed / 2);
        }
    }
    private void getClimpForMobile()
    {
        //Check Is trigger in box collider 2d to go throught
        if (isLadder() && isClimpButton == true)
        {
            body.velocity = new Vector2(body.velocity.x, 1 * speed / 2);
        }
    }
    public bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }
    public bool isLadder()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, ladderLayer);
        return raycastHit2D.collider != null;
    }
    public bool canAttack()
    {
        return !onWall();
    }

    public void leftButton()
    {
        isLeftButton = true;
        isRightButton = false;
    }
    public void rightButton()
    {
        isLeftButton = false;
        isRightButton = true;
    }
    public void noneButton()
    {
        isLeftButton = false;
        isRightButton = false;
    }
    public void noneclimpButton()
    {
        isClimpButton = false;
    }
    public void climpButton()
    {
        isClimpButton = true;
    }
}
