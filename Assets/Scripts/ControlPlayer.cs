using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPlayer : MonoBehaviour
{

    public float Speed = 10;

    //create a Vector3 variable to store our eixoX and eixoZ
    private Vector3 direction;

    /*Masks everything except the ground, this will prevent our player rotation to not work properly
    because the hitboxes from the structures */
    public LayerMask GroundMask;

    /*Create a variable that contains the UI to Death Screen*/
    public GameObject gameOverText;

    /*Create a variable that contains the player's life*/
    public bool isAlive = true;
    /*To avoid the problem that when we respawn our game is paused we need to make the Start function calls timescale = 1*/

    private Rigidbody rigidbodyPlayer;
    private Animator animatorPlayer;
    private void Start(){
        Time.timeScale = 1;
        rigidbodyPlayer = GetComponent<Rigidbody>();
        animatorPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*change x and y to move the player using Translate, inserting the vector X,Y and Z with Vector3 (3 dimensions)
        
        first, we gonna read the button input
        GetAxis("Horizontal") -> X and ("Vertical") -> Z is a native function from Unity 
        */

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        direction = new Vector3(eixoX, 0, eixoZ);
       
        /*condition to change idle animation to run animation when pressed a key
        GetComponent to access animator */

        if(direction != Vector3.zero){
            animatorPlayer.SetBool("Movement", true);
        }else{
            animatorPlayer.SetBool("Movement", false);
        }

        /*Let's see if our player is alive. If it isn't alive then we let him restart the game by clicking the mouse or Ctrl.*/
        if(isAlive == false){
            if(Input.GetButtonDown("Fire1")){
                SceneManager.LoadScene("game");
            }
        }
    }

    /*Fixed update is called every 0,02sec
    When we use transform there is a problem with the physics and the camera, to solve this we gonna take the player 
    position from the rigidbody
    to acess the rigidbody*/
    void FixedUpdate() {
        rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + 
        (direction * Speed * Time.deltaTime));

        /*Rotate the player using the mouse pointer
        acess main camera and take the mousePosition*/
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        //to acess the ray, enter the unity debug
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        /*to make the player rotate, we gonna make the player character looks in the direction of the ray where 
        it touches the ground*/
        RaycastHit impact;
        if(Physics.Raycast(raio, out impact, 100, GroundMask)){
            Vector3 positionAimPlayer = impact.point - transform.position;

            //cancel the possibility to the player looks to the ground or to the sky
            positionAimPlayer.y = transform.position.y;

            Quaternion newRotation = Quaternion.LookRotation(positionAimPlayer);
            rigidbodyPlayer.MoveRotation(newRotation);
        }
    }
}
