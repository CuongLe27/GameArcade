using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBehindLadder : MonoBehaviour
{
    [SerializeField] private Collider2D Player;
    [SerializeField] private PlayerMovement playerMovement;
    private Collider2D GroundBehind;
    private void Awake()
    {
        GroundBehind= GetComponent<Collider2D>();
    }
    private void Update()
    {
        if(playerMovement.isLadder()==true && Player.IsTouching(GroundBehind) == true)
        {
            GroundBehind.isTrigger = true;
        }else
        {
            GroundBehind.isTrigger = false;
        }
        
    }
}
