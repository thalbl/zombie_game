using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{   
     private int healingNumber = 15;
     private int destructionTime = 5;

    private void Start() {
        Destroy(gameObject, destructionTime);
    }
    //When the player touches the medkit the trigger activates
    private void OnTriggerEnter(Collider collisionObject) {
        if(collisionObject.tag == "Player"){
            collisionObject.GetComponent<ControlPlayer>().HealHealth(healingNumber);
            Destroy(gameObject);
        }
    }
}
