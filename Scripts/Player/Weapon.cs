using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider2D BoxCollider2D;
    private void Awake()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }
    public void setDirec(float _direc)
    {
        BoxCollider2D.enabled = true;
        gameObject.SetActive(true);
        float localScale = transform.localScale.x;
        if (Mathf.Sign(localScale) != _direc)
            localScale = -localScale;
        transform.localScale = new Vector3(localScale, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BoxCollider2D.enabled = false;
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().takeDamage(1);
        }
    }
}
