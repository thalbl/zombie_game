using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public float timeToNextSpawn = 0;
    private float timeBetweenSpawn = 10;
    public GameObject BossPrefeb;

    private ControlInterface controlInterfaceScript;

    public Transform[] PossibleGenerationPositions;
    private Transform player;


    private void Start() {
        timeToNextSpawn = timeBetweenSpawn;
        controlInterfaceScript = GameObject.FindObjectOfType(typeof(ControlInterface)) as ControlInterface;
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update(){
        if(Time.timeSinceLevelLoad > timeToNextSpawn){
            Vector3 positionToSpawnBoss = CalculateFurthestPositionFromPlayer();
            Instantiate(BossPrefeb, positionToSpawnBoss, Quaternion.identity);
            controlInterfaceScript.ShowNewBoss();
            timeToNextSpawn = Time.timeSinceLevelLoad + timeBetweenSpawn;
        }
    }

    Vector3 CalculateFurthestPositionFromPlayer(){
        Vector3 positionAway = Vector3.zero;
        float biggestDistance = 0;

        foreach(Transform position in PossibleGenerationPositions)
        {
            float distanceBetweenPlayer = Vector3.Distance(position.position, player.position);

            if(distanceBetweenPlayer > biggestDistance)
            {
                biggestDistance = distanceBetweenPlayer;
                positionAway = position.position;
            }
        }
        return positionAway;
    }
}
