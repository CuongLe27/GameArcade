using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerResprawn : MonoBehaviour
{
    private Transform currentCheckPoint;
    private Health playerHealth;
    private bool PreviousScence;
    private void Awake()
    {
        //if error use code below
        //PlayerPrefs.SetString("PreviousScence?", "False");
        //PlayerPrefs.SetString("PreviousCamScence?", "False");
        //PlayerPrefs.Save();
        playerHealth = GetComponent<Health>();
        if (PlayerPrefs.GetString("PreviousScence?") =="True")
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            transform.position = new Vector2(x, y);
            transform.localScale =new Vector2(-1, 1);
        }
        PlayerPrefs.SetString("PreviousScence?", "False");
        PlayerPrefs.Save();
    }
    private void Start()
    {
        
    }
    public void RespawnCheckpoint()
    {
        //Move player to checkpoint
        if (currentCheckPoint != null)
        {
            transform.position = currentCheckPoint.position;
        }
        else
        {
            Debug.LogWarning("No checkpoint set!");
        }
        playerHealth.Respawn();
        //Move camera to checkpoint
        Camera.main.GetComponent<CameraController>().moveToNextRoom(currentCheckPoint?.parent);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag=="Checkpoint")
        {
            currentCheckPoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
        }
        else if( collision.transform.tag == "NextScence")
        {
            // Save location before load scence
            PlayerPrefs.SetFloat("CheckpointX", transform.position.x-0.5f);
            PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
            //Save Cam position
            Transform roomCam = Camera.main.transform;
            PlayerPrefs.SetFloat("CamX", roomCam.position.x);
            PlayerPrefs.SetString("PreviousScence?", "False");
            PlayerPrefs.SetString("PreviousCamScence?", "False");
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.transform.tag == "PreviousScence")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            PlayerPrefs.SetString("PreviousScence?", "True");
            PlayerPrefs.SetString("PreviousCamScence?", "True");
            PlayerPrefs.Save();
        }
    }
}
