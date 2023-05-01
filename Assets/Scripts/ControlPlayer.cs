using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlPlayer : MonoBehaviour, ITakeDamage, IHealing
{

    //create a Vector3 variable to store our eixoX and eixoZ
    private Vector3 direction;

    /*Masks everything except the ground, this will prevent our player rotation to not work properly
    because the hitboxes from the structures */
    public LayerMask GroundMask;

    /*Create a variable that contains the UI to Death Screen*/
    public GameObject gameOverText;

    public ControlInterface scriptControlInterface;
    public AudioClip DamageSound;
    private CharacterAnimation playerAnimation;
    public Status playerStatus;
    private PlayerMovement myPlayerMovement;

     /*To avoid the problem that when we respawn our game is paused we need to make the Start function calls timescale = 1*/
    private void Start(){
        Time.timeScale = 1;
        myPlayerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<CharacterAnimation>();
        playerStatus = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        /*change x and y to move the player using Translate, inserting the vector X,Y and Z with Vector3 (3 dimensions)
        
        first, we gonna read the button input
        GetAxis("Horizontal") -> X and ("Vertical") -> Z is a native function from Unity 
        */

        float eixoX = Input.GetAxisRaw("Horizontal");
        float eixoZ = Input.GetAxisRaw("Vertical");
        direction = new Vector3(eixoX, 0, eixoZ);
       
        /*condition to change idle animation to run animation when pressed a key
        GetComponent to access animator */

        playerAnimation.Moving(direction.magnitude);
        
    }

    /*Fixed update is called every 0,02sec
    When we use transform there is a problem with the physics and the camera, to solve this we gonna take the player 
    position from the rigidbody
    to acess the rigidbody*/
    void FixedUpdate() {
        myPlayerMovement.Movement(direction, playerStatus.Speed);

        myPlayerMovement.RotatePlayer(GroundMask);
        
    }
    public void TakeDamage(int damage){
        playerStatus.CurrentHealth -= damage;
        scriptControlInterface.UpdateHealthBar();
        ControlAudio.instance.PlayOneShot(DamageSound);

        /*When the player gets hit, we gonna pause the game, timeScale acess the Time from the game
        To make clear to the player that it is a game over, we need to create a game over interface in unity

        To trigger the UI text, we need to create a variable that will contain the GameOver text in ControlPlayer and then
        set it active here */
        if(playerStatus.CurrentHealth <= 0){
            Dies();
        }
        }

        public void Dies(){ 
            scriptControlInterface.GameOver();
        }

        //calls the HealHealth function in IHealing interface
        public void HealHealth(int howMuchHeal){
            playerStatus.CurrentHealth += howMuchHeal;

            //condition to not allow that player heals more than 100hp
            if(playerStatus.CurrentHealth > playerStatus.StarterHealth){
                playerStatus.CurrentHealth = playerStatus.StarterHealth;
            }
            scriptControlInterface.UpdateHealthBar();
        }
        
}
