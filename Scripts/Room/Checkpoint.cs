using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int numOffFlashes;
    private int flag = 0;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(WaitTime());
    }
    IEnumerator WaitTime()
    {
        for (int i = 0; i <= numOffFlashes; i++)
        {
            if (flag == 0)
            {
                transform.localScale = new Vector3(transform.localScale.x + 0.2f, transform.localScale.y + 0.3f);
                flag = 1;
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.2f, transform.localScale.y - 0.3f);
                flag = 0;
            }
           yield return new WaitForSeconds(0.5f);
        }
        
    }
}
