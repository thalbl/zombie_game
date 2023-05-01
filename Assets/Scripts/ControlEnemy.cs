using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemy : MonoBehaviour, ITakeDamage
{
    public GameObject Player;
    private CharacterAnimation animatorZombie;
    private MovementCharacter zombieMovement;
    private Status zombieStatus;
    public AudioClip DestroyZombie;
    private Vector3 randomPosition;
    private Vector3 direction;
    private float walkingCount;
    private float timeToWalk = 4;

    //percentage that a zombie can drop a medkit when dies, in this case it's 10%
    private float percentageMedKit = 0.25f;
    public GameObject MedKitPrefab;
    private ControlInterface scriptControlInterface;

    [HideInInspector]
    public ZombieGenerator myGenerator;

    public GameObject BloodParticleZombie;

    // Start is called before the first frame update
    void Start()
    {   
        animatorZombie = GetComponent<CharacterAnimation>();
        Player = GameObject.FindWithTag("Player");
        zombieMovement = GetComponent<MovementCharacter>();
        RandomZombie();
        zombieStatus = GetComponent<Status>();
        scriptControlInterface = GameObject.FindObjectOfType(typeof(ControlInterface)) as ControlInterface;
    }


    void FixedUpdate(){
        //make zombie chase the player
        
        float distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);


        //Quaternion is a structure in unity that has x,y and z and a imaginy axys to calculate the rotation
        zombieMovement.Rotation(direction);
        animatorZombie.Moving(direction.magnitude);
        //make zombie agro radius

        if(distanceFromPlayer > 15){
            WalkingAround();
        }
        //make zombie rotate and stop to attack when reach the player
        else if(distanceFromPlayer > 2.5){
            direction = Player.transform.position - transform.position;
            zombieMovement.Movement(direction, zombieStatus.Speed);
            animatorZombie.Attack(false);
            } 
        //make zombie attacks when its close to the player
        else{
            direction = Player.transform.position - transform.position;
            animatorZombie.Attack(true);
        }
    }

    
    Vector3 RandomPosition(){
        //Random.insideUnitSphere takes a sphere from radio 1 and generate a random position
        Vector3 position = Random.insideUnitSphere * 10;
        position += transform.position;
        
        //cancels the y position generated in insideUnitSphere
        position.y = transform.position.y;
        return position;
    }

    void WalkingAround(){
        walkingCount -= Time.deltaTime;
        if(walkingCount <= 0){
            randomPosition = RandomPosition();
            walkingCount += timeToWalk + Random.Range(-1f, 1f);
        }
        bool isClose = Vector3.Distance(transform.position, randomPosition) <= 0.05;
        if(isClose == false){
            direction = randomPosition - transform.position;
            zombieMovement.Movement(direction, zombieStatus.Speed);
        }
        
    }

    /*To make the zombie hits the player, using the event AttacksPlayer created on the attack animation*
    The method created here needs to have the same name from the animation event created in Unity*/
    void AttacksPlayer(){
        int damage = Random.Range(20, 30);
        Player.GetComponent<ControlPlayer>().TakeDamage(damage);
    }

    void RandomZombie(){
        int generateZombieType = Random.Range(1, transform.childCount);
        transform.GetChild(generateZombieType).gameObject.SetActive(true);
        
    }

    public void TakeDamage(int damage){
        zombieStatus.CurrentHealth -= damage;
        if(zombieStatus.CurrentHealth <= 0){
            Dies();
        }
    }

    public void BloodParticle(Vector3 position, Quaternion rotation){
        Instantiate(BloodParticleZombie ,position, rotation);
    }

    public void Dies()
    {   
        animatorZombie.Dies();
        zombieMovement.Dies();
        Destroy(gameObject, 2);
        this.enabled = false;
        ControlAudio.instance.PlayOneShot(DestroyZombie);
        checkMedKitGeneration(percentageMedKit);
        
        scriptControlInterface.UpdateKilledZombies();
        myGenerator.ReduceAliveZombiesAmount();   
    }

    //when the zombie dies, there is chance that it drops a medkit
    void checkMedKitGeneration(float chancePercentage){
        //Random.value returns a value within 0.0 and 1.0, its better for percentages 
        if(Random.value <= chancePercentage){
            Instantiate(MedKitPrefab, transform.position, Quaternion.identity);
        }
    }
}
