using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    public float Speed = 30;
    private Rigidbody rigidbodyBullet;

    private void Start() {
        rigidbodyBullet = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigidbodyBullet.MovePosition(rigidbodyBullet.position + 
        transform.forward * Speed * Time.deltaTime);
    }

    /*To make the bullet trigger self destruction or the zombie destruction*/

    void OnTriggerEnter(Collider collisionObject) {
        if(collisionObject.tag == "Enemy"){
            Destroy(collisionObject.gameObject);
            
        }
        Destroy(gameObject);
    }
}
