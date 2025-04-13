using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOffFlashes;
    private SpriteRenderer SpriteRend;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        SpriteRend = GetComponent<SpriteRenderer>();
    }

    public void takeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("Dead");
                //Check which sprite use this scripts. If player
                if(GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                }
                //If enemy
                if (GetComponentInParent<GoblinPatrol>() != null)
                {
                    GetComponentInParent<GoblinPatrol>().enabled = false;
                }
                if (GetComponent<Goblin>() != null)
                {
                    GetComponent<Goblin>().enabled = false;
                }
                if (GetComponent<GoblinDodge>() != null)
                {
                    GetComponent<GoblinDodge>().enabled = false;
                }
                dead = true;
            }

        }
    }
    public void addHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public void Respawn()
    {
        dead = false;
        addHealth(startingHealth);
        anim.ResetTrigger("Dead");
        anim.Play("Idle");
        if (GetComponent<PlayerMovement>() != null)
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberOffFlashes; i++)
        {
            SpriteRend.color = new Color(1, 0, 0, 0.8f);
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));
            SpriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));

        }
        Physics2D.IgnoreLayerCollision(9, 10, false);

    }
}
