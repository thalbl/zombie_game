using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//To make our boss follow the player but bypass the scenario obstacles, we used NavMesh and added a AI to it
public class ControlBoss : MonoBehaviour, ITakeDamage
{
    private Transform player;
    private NavMeshAgent agent; 
    private Status bossStatus;
    private CharacterAnimation bossAnimation;
    private MovementCharacter bossMovement;
    public GameObject MedKitPrefeb;
    public Slider sliderBossHealth;
    public Image colorSliderImage;
    public Color maxHealthColor, minHealthColor;
    public GameObject BloodParticleBoss;

    private void Start() {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        bossStatus = GetComponent<Status>();
        bossAnimation = GetComponent<CharacterAnimation>();
        agent.speed = bossStatus.Speed;

        bossMovement = GetComponent<MovementCharacter>();
        sliderBossHealth.maxValue = bossStatus.StarterHealth;
        UpdateInterface();
    }

    private void Update(){
        
    }

    private void FixedUpdate() {
        //set to follow the player position
        agent.SetDestination(player.position);
        //set walk animation
        bossAnimation.Moving(agent.velocity.magnitude);

        /*make it attacks, with the mesh agent it's possible to know if we are close using remainingDistance and 
        stoppingDistance.
        
        remainingDistance is how much does it takes to go to stoppingDistance

        */
        if(agent.hasPath == true){
            bool bossIsCloseToThePlayer = agent.remainingDistance <= agent.stoppingDistance;
            if(bossIsCloseToThePlayer){
                bossAnimation.Attack(true);             
            }
            else{
                transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
                bossAnimation.Attack(false);
            }
        }
    }

    void AttacksPlayer(){
        int damage = Random.Range(1, 1);
        player.GetComponent<ControlPlayer>().TakeDamage(damage);
    }

    public void TakeDamage(int damage){
        bossStatus.CurrentHealth -= damage;
        UpdateInterface();
        if(bossStatus.CurrentHealth <= 0){
            Dies();
        }
    }
     public void BloodParticle(Vector3 position, Quaternion rotation){
        Instantiate(BloodParticleBoss ,position, rotation);
    }

    public void Dies()
    {
        bossAnimation.Dies();
        bossMovement.Dies();
        this.enabled = false;
        agent.enabled = false;
        Instantiate(MedKitPrefeb, transform.position, Quaternion.identity);
        Destroy(gameObject, 2);   
    }

    void UpdateInterface(){
        sliderBossHealth.value = bossStatus.CurrentHealth;

        float healthPercent = (float)bossStatus.CurrentHealth / bossStatus.StarterHealth;
        //Linear interpolation, used to make color transitions (not only)
        Color healthColor = Color.Lerp(minHealthColor, maxHealthColor, healthPercent);
        colorSliderImage.color = healthColor;
    }
}
