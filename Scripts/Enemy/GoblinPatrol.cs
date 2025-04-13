using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEadge;
    [SerializeField] private Transform rightEadge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameter")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behavior")]
    //Time to stop when stan in patrol (/s); 
    [SerializeField] private float idleDuration;
    private float idleTimer; 

    [Header("Moving")]
    [SerializeField] private Animator anim;
    private void Awake()
    {
        initScale = enemy.localScale;
    }

    // When player in sight attack stop moving animation. (See disable in Goblin.cs)
    private void OnDisable()
    {
        anim.SetBool("Moving", false);
    }
    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftEadge.position.x)
            {
                MovingInDirection(-1);
            }
            else{
                DirectionChange(); 
            }
        }
        else
        {
            if (enemy.position.x <= rightEadge.position.x)
            {
                MovingInDirection(1);
            }
            else{
                DirectionChange();
            }
        }
    }
    private void DirectionChange()
    {
        anim.SetBool("Moving", false);

        idleTimer += Time.deltaTime;
        if(idleTimer>idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }
    private void MovingInDirection(int _direction)  
    {
        idleTimer = 0;
        anim.SetBool("Moving",true);
        //Make enemy face right direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        //Move in direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
