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
    private int aliveZombiesLimit = 2;
    private int currentAliveZombies;

    //variables for increase zombie generation by the time passes
    private float timeNextDifficult = 20;
    private float increaseDifficultCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        increaseDifficultCount = timeNextDifficult;
        for(int i = 0; i < aliveZombiesLimit ; i++){
            StartCoroutine(GenerateNewZombie());
        }
    }

    // Update is called once per frame
    void Update()
    {   
        bool canIGenZombiesByDistance = Vector3.Distance(transform.position, player.transform.position) > generationDistanceFromPlayer;
        if(canIGenZombiesByDistance == true && currentAliveZombies < aliveZombiesLimit){
            timeCount += Time.deltaTime;

            if(timeCount >= zombieGenerateTime){
                StartCoroutine(GenerateNewZombie());
                timeCount = 0;
            }  
        }

        if(Time.timeSinceLevelLoad > increaseDifficultCount){
            aliveZombiesLimit++;
            increaseDifficultCount = Time.timeSinceLevelLoad + timeNextDifficult;
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
        ControlEnemy zombie = Instantiate(Zombie, creationPosition, transform.rotation).GetComponent<ControlEnemy>();
        zombie.myGenerator = this;
        currentAliveZombies++;
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

    public void ReduceAliveZombiesAmount(){
        currentAliveZombies--;
    }
}
