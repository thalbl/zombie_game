using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementCharacter
{   
    public GameObject positionAim;
    public void RotatePlayer(LayerMask GroundMask){

        /*Rotate the player using the mouse pointer
        acess main camera and take the mousePosition*/
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        Plane plane = new Plane(Vector3.up, positionAim.transform.position);
        //to acess the ray, enter the unity debug
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);
        float collisionDistance;
        
        if(plane.Raycast(raio, out collisionDistance)){
            Vector3 positionAimPlayer = raio.GetPoint(collisionDistance);
            positionAimPlayer.y = 0; // or transform.position.y = 0

            Vector3 directionToAim = positionAimPlayer - transform.position;

            Quaternion novaRotacao = Quaternion.LookRotation(directionToAim);
            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }
}
