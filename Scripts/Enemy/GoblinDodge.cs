using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDodge : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private float JumpPower;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;

    private Animator anim;
    private Health playerHealth;
    private GoblinPatrol goblinPatrol;
    private Rigidbody2D body;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        goblinPatrol = GetComponentInParent<GoblinPatrol>();
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        coolDownTimer += Time.deltaTime;
        if (playerInSight())
        {
            if (coolDownTimer >= attackCoolDown)
            {
                coolDownTimer = 0;
                anim.SetTrigger("DodgeAttack");
            }
        }
        // When player in sight attack disable goblinPatrol to stop goblin moving.
        if (goblinPatrol != null)
        {
            goblinPatrol.enabled = !playerInSight();
        }
    }
    private bool playerInSight()
    {
        
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }
    private void GetJump()
    {
        if (transform.localScale.x > 0)
            body.velocity = new Vector2(body.velocity.x - 7, JumpPower);
        else
            body.velocity = new Vector2(body.velocity.x + 7, JumpPower);
    }
    private void GetDash()
    {
        if(transform.localScale.x > 0)
            transform.position = new Vector2(body.position.x + range + 2, body.position.y);
        else 
            transform.position = new Vector2(body.position.x - range - 2        , body.position.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void TakeDamage()
    {
        if (playerInSight())
        {
            playerHealth.takeDamage(damage);
        }
    }
}
