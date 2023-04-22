using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int StarterHealth = 100;
    //Hide in spector hides the variable in the inspector
    [HideInInspector]
    public int CurrentHealth;
    public float Speed = 5;

    void Awake(){
        CurrentHealth = StarterHealth;
    }

}
