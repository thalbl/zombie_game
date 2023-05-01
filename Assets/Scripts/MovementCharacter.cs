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
        var speed = 20;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, speed * Time.deltaTime, 0.0f);
        Quaternion newRotation = Quaternion.LookRotation(direction);
        myRigidbody.MoveRotation(newRotation);
   }

   public void Dies(){
    myRigidbody.constraints = RigidbodyConstraints.None;
    myRigidbody.velocity = Vector3.zero;
    myRigidbody.isKinematic = false; 
    GetComponent<Collider>().enabled = false;
   }

}
