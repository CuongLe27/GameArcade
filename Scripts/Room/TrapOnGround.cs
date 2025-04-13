using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOnGround : MonoBehaviour
{
    [SerializeField] private PlayerMovement PlayerMovement;
    private Collider2D ColliderGround;
    private SpriteRenderer SpriteGround;
    
    private void Awake()
    {
        ColliderGround = GetComponent<Collider2D>();
        SpriteGround = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!PlayerMovement.isGrounded())
        {
            ColliderGround.isTrigger = false;
            SpriteGround.maskInteraction = SpriteMaskInteraction.None;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            ColliderGround.isTrigger = true;
            SpriteGround.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }
}
