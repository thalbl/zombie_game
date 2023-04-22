using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject Zombie;
    private float timeCount = 0;
    public float zombieGenerateTime = 1;
    public LayerMask ZombieMask;
    private float generationDistance = 3;
    private float generationDistanceFromPlayer = 20;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > generationDistanceFromPlayer){
            timeCount += Time.deltaTime;

            if(timeCount >= zombieGenerateTime){
                StartCoroutine(GenerateNewZombie());
                timeCount = 0;
            }  
        }
    }

    //Coroutine
    IEnumerator GenerateNewZombie(){
        Vector3 creationPosition = RandomizePosition(); 
        Collider[] colisores = Physics.OverlapSphere(creationPosition, 1, ZombieMask);

        while(colisores.Length > 0){
            RandomizePosition(); 
            Physics.OverlapSphere(creationPosition, 1, ZombieMask);
            //when we use while, theres a chance that unity crashes, to avoid it, we can use yield return null
            yield return null;
        }
        Instantiate(Zombie, creationPosition, transform.rotation);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, generationDistance);
    }

    Vector3 RandomizePosition(){
        Vector3 position = Random.insideUnitSphere * generationDistance;
        position += transform.position;
        position.y = 0;
        return position;
    }
}
