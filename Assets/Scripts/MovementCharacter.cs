using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    private Rigidbody myRigidbody;

    void Awake(){
        myRigidbody = GetComponent<Rigidbody>();
    }
    public void Movement(Vector3 direction, float speed){
        myRigidbody.MovePosition(myRigidbody.position + 
        direction.normalized * speed * Time.deltaTime);     
   }

   public void Rotation(Vector3 direction){
    Quaternion newRotation = Quaternion.LookRotation(direction);
        myRigidbody.MoveRotation(newRotation);
   }

}
