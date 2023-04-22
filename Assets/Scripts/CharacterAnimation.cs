using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator myAnimator;

    void Awake() {
        myAnimator = GetComponent<Animator>();
    }
   public void Attack(bool status){
    myAnimator.SetBool("Attacking", status);
   }

   public void Moving(float movingValue){
    myAnimator.SetFloat("Movement", movingValue);
   }
}
