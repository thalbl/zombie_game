using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject Zombie;
    private float timeCount = 0;
    public float zombieGenerateTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount >= zombieGenerateTime){
            Instantiate(Zombie, transform.position, transform.rotation);
            timeCount = 0;
        }
        
    }

    private void FixedUpdate() {
        
    }
}
