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

    // Start is called before the first frame update
    void Start()
    {   
        animatorZombie = GetComponent<CharacterAnimation>();
        Player = GameObject.FindWithTag("Player");
        zombieMovement = GetComponent<MovementCharacter>();
        RandomZombie();
        zombieStatus = GetComponent<Status>();
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
            walkingCount += timeToWalk;
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
        int generateZombieType = Random.Range(1, 28);
        transform.GetChild(generateZombieType).gameObject.SetActive(true);
        
    }

    public void TakeDamage(int damage){
        zombieStatus.CurrentHealth -= damage;
        if(zombieStatus.CurrentHealth <= 0){
            Dies();
        }
    }

    public void Dies()
    {
        Destroy(gameObject);
        ControlAudio.instance.PlayOneShot(DestroyZombie);
    }
}
