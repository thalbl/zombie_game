using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGun : MonoBehaviour
{   
    public GameObject Bullet;
    public GameObject CanoDaArma;
    // Start is called before the first frame update
    public AudioClip ShootSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //if condition to know when the player clicks mouse button or Ctrl -> Fire1
        if(Input.GetButtonDown("Fire1")){
            Instantiate(Bullet, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
            ControlAudio.instance.PlayOneShot(ShootSound);
        }
    }
}
