using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    //to make the camera follow our player we need to create a variable using GameObject that will be a reference to 
    //our player
    public GameObject Player;
    private Vector3 distanceFromPlayer;

    // Start is called before the first frame update
    //We will calculate the distance the camera is from the player in the Start
    void Start()
    {
        distanceFromPlayer = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //acess the Player position with transform.position and attributes it to the camera position
        transform.position = Player.transform.position + distanceFromPlayer;
    }
}
