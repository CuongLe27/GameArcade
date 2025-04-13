using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float AttackCoolDown;
    [SerializeField] private Transform meleePoint;
    [SerializeField] private GameObject melee;
    private Animator anim;
    private PlayerMovement PlayerMovement;
    private float timeCoolDown;
    private bool isAttackButton;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetKey("j") && timeCoolDown > AttackCoolDown && PlayerMovement.canAttack() || isAttackButton == true &&
            timeCoolDown > AttackCoolDown && PlayerMovement.canAttack())
        {
            Attack();
        }
        timeCoolDown += Time.deltaTime;
    }
    private void Attack()
    {
        anim.SetTrigger("attack");
        timeCoolDown = 0;
        melee.transform.position = meleePoint.position;
        melee.GetComponent<Weapon>().setDirec(Mathf.Sign(transform.localScale.x));
    }
    public void Deactivate()
    {
        melee.gameObject.SetActive(false);
    }
    public void noneButton()
    {
        isAttackButton = false;
    }
    public void attackButton()
    {
        isAttackButton = true;
    }
}
