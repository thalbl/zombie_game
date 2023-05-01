using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    public float Speed = 30;
    private Rigidbody rigidbodyBullet;
    public AudioClip DestroyZombie;

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
        Quaternion BulletRotation = Quaternion.LookRotation(-transform.forward);
        switch(collisionObject.tag){
            case "Enemy":
            ControlEnemy enemy = collisionObject.GetComponent<ControlEnemy>();
            enemy.TakeDamage(1);
            enemy.BloodParticle(transform.position, BulletRotation);
            
            break;

            case "Boss":
            ControlBoss boss = collisionObject.GetComponent<ControlBoss>();
            boss.TakeDamage(1);
            boss.BloodParticle(transform.position, BulletRotation);
            break;
        }
        Destroy(gameObject);
    }
}
