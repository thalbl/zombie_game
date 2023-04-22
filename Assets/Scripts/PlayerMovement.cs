using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementCharacter
{
    public void RotatePlayer(LayerMask GroundMask){

        /*Rotate the player using the mouse pointer
        acess main camera and take the mousePosition*/
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        //to acess the ray, enter the unity debug
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        /*to make the player rotate, we gonna make the player character looks in the direction of the ray where 
        it touches the ground*/
        RaycastHit impact;
        if(Physics.Raycast(raio, out impact, 100, GroundMask)){
            Vector3 positionAimPlayer = impact.point - transform.position;

            //cancel the possibility to the player looks to the ground or to the sky
            positionAimPlayer.y = transform.position.y;

            Rotation(positionAimPlayer);
        }
    }
}
