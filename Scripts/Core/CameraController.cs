using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room Camera
    [SerializeField] private float speed;
    public float currentPostX;
    private Vector3 velocity = Vector3.zero;
    //Follow player
    [SerializeField] Transform player;
    private void Start()
    {
         //See more in PlayerRespawn.cs// fix cam when teleport
        if (PlayerPrefs.GetString("PreviousCamScence?") == "True")
        {

            currentPostX = PlayerPrefs.GetFloat("CamX");
            
        }
        PlayerPrefs.SetString("PreviousCamScence?", "False");
        PlayerPrefs.Save();
    }
    void Update()
    {
       
        //Room camera
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPostX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follow player
        //transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
    public void moveToNextRoom(Transform _newRoom)
    {
        currentPostX = _newRoom.position.x;
    }
    public void moveToNextLevel(Transform _newRoom)
    {
        currentPostX = _newRoom.position.x;
        //Transform player when move next room 
        if (_newRoom.position.x < 1 && _newRoom.position.x > -1)
            player.transform.position = new Vector2(-1 * 6, player.position.y);
        else if (_newRoom.position.x < -1)
            player.transform.position = new Vector2(_newRoom.position.x * 6, player.position.y);
        else
            player.transform.position = new Vector2(_newRoom.position.x / 1.7f, player.position.y);
    }
}
