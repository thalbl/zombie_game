using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemy : MonoBehaviour
{
    public GameObject Player;
    public float SpeedZombie = 5;
    private Rigidbody rigidbodyZombie;
    private Animator animatorZombie;

    // Start is called before the first frame update
    void Start()
    {   
        rigidbodyZombie = GetComponent<Rigidbody>();
        animatorZombie = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        int generateZombieType = Random.Range(1, 28);
        transform.GetChild(generateZombieType).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        //make zombie chase the player
        
        float distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);

        Vector3 direction = Player.transform.position - transform.position;

        //Quaternion is a structure in unity that has x,y and z and a imaginy axys to calculate the rotation
        Quaternion newRotation = Quaternion.LookRotation(direction);
        rigidbodyZombie.MoveRotation(newRotation);
        
        //make zombie rotate and stop to attack when reach the player
        if(distanceFromPlayer > 2.5){
            rigidbodyZombie.MovePosition(rigidbodyZombie.position + 
            direction.normalized * SpeedZombie * Time.deltaTime);
            animatorZombie.SetBool("Attacking", false);
            }
        //make zombie attacks when its close to the player
        else{
            animatorZombie.SetBool("Attacking", true);
        }
    }

    /*To make the zombie hits the player, using the event AttacksPlayer created on the attack animation*
    The method created here needs to have the same name from the animation event created in Unity*/
    void AttacksPlayer(){
        /*When the player gets hit, we gonna pause the game, timeScale acess the Time from the game
        To make clear to the player that it is a game over, we need to create a game over interface in unity

        To trigger the UI text, we need to create a variable that will contain the GameOver text in ControlPlayer and then
        set it active here*/
        Time.timeScale = 0;
        Player.GetComponent<ControlPlayer>().gameOverText.SetActive(true);

        /*Same thing to make it restart the game when the player dies. Create a variable in ControlPlayer and calls it here*/
        Player.GetComponent<ControlPlayer>().isAlive = false;
    }
}
