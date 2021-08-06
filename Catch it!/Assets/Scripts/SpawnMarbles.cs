using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMarbles : MonoBehaviour
{
    public MarbleCollector marbleCollector;

    public int randomSpawn;
    public List<int> randomNumbers;
    public bool match = false;

    private float spawnPosX = 2;
    private float spawnPosY = 6;
    private float spawnPosZ = -1.915f;
    private float waitingTime = 0.2f;
    public float localTime;
    public float globalTime;
    public GameManager gameManager;
    public LevelDesign Levels;
    public int marbleCounter = 1;
    private void Start()
    {
        localTime = 0f;
        globalTime = 0f;
    }
    void FixedUpdate()
    {
        globalTime += Time.deltaTime;
        localTime += Time.deltaTime;
        if (!gameManager.gameOver && marbleCounter <= Levels.marbleLooseCount)
        {          
            TimeToSpawn();
            
        }
        if (gameManager.gameOver)
        {
            List<GameObject> marble = ObjectPooler.SharedInstance.GetPooledObject();
            for(int i = 0; i < marble.Count; i++)
            {
                marble[i].SetActive(false);
            }
        }
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(waitingTime);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnPosX, spawnPosX), spawnPosY, spawnPosZ);
        List<GameObject> marble = ObjectPooler.SharedInstance.GetPooledObject();
        randomSpawn = Random.Range(0, marble.Count);
        MatchController();
        randomNumbers.Add(randomSpawn);
        
        void MatchController()
        {
            for (int i = 0; i < randomNumbers.Count; i++)
            {
                if (randomNumbers[i] == randomSpawn)
                {
                    Debug.Log("NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN1");
                    match = true;
                    break;
                }
            }
            if (match)
            {
                randomSpawn = Random.Range(0, marble.Count);
                match = false;
                MatchController();
            }
        }
        
        
        if (marble != null && !match)
        {
            marble[randomSpawn].transform.position = spawnPos;
            marble[randomSpawn].transform.rotation = marble[randomSpawn].transform.rotation;
            marble[randomSpawn].SetActive(true);
            marbleCounter++;
        }

    }

    public void TimeToSpawn()
    {
        //Takes Black Marble
        if(marbleCollector.speedUp == true && marbleCollector.slowDown == false && localTime >= 0.75f && globalTime < 3)
        {
            
            StartCoroutine(Spawn());
            Debug.Log("BLACK MARBLE " + marbleCounter);
            localTime = 0f;
            if (globalTime >= 2.5f)
            {
                marbleCollector.speedUp = false;
            }
        }
        //Takes Green Marble
        else if (marbleCollector.slowDown == true && marbleCollector.speedUp == false && localTime >= 3f && globalTime < 10)
        {
            
            StartCoroutine(Spawn());
            Debug.Log("GREEN MARBLE " + marbleCounter);
            localTime = 0f;

            if (globalTime >= 9f)
            {
                marbleCollector.slowDown = false;
            }
        }
        //Takes Normal Marble
        else if (marbleCollector.slowDown == false && marbleCollector.speedUp == false && localTime >= 1.75f)
        {
            StartCoroutine(Spawn());
            Debug.Log("Marble Counter: " + marbleCounter);
            localTime = 0f;
        }
        
        
        
        
        /*if (localTime >= 3f && globalTime < 15)
        {
            StartCoroutine(Spawn());
            localTime = 0f;
        }
        else if(localTime >= 2f && globalTime > 15 && globalTime < 35)
        {
            StartCoroutine(Spawn());
            localTime = 0f;
        }
        else if (localTime >= 1.5f && globalTime > 35 && globalTime < 70)
        {
            StartCoroutine(Spawn());
            localTime = 0f;
        }
        else if (localTime >= 1f && globalTime > 70 && globalTime < 130)
        {
            StartCoroutine(Spawn());
            localTime = 0f;
        }
        else if (localTime >= 0.75f && globalTime > 130 && globalTime < 160)
        {
            StartCoroutine(Spawn());
            localTime = 0f;
        }
        else if (localTime >= 0.5f && globalTime > 160 && globalTime < 190)
        {
            StartCoroutine(Spawn());
            localTime = 0f;
        }
        else if (localTime >= 0.25f && globalTime > 190 && globalTime < 220)
        {
            StartCoroutine(Spawn());
            localTime = 0f;
        }
        else if (localTime >= 0.1f && globalTime > 220)
        {
            StartCoroutine(Spawn());
            localTime = 0f;
        }*/
    }
}
