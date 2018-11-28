using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public List<GameObject> AI = new List<GameObject>();
    public int maxAI;

    public float spawnCooldown;

    public Transform spawnLocation;

    public GameObject aiPrefab;

    private bool crRunning;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject ai in GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (ai.gameObject.layer == LayerMask.NameToLayer("AI"))
            {
                AI.Add(ai);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AI.Count < maxAI && !crRunning)
        {
            StartCoroutine(WaitAndSpawn());

        }
    }

    IEnumerator WaitAndSpawn()
    {
        crRunning = true;
        yield return new WaitForSeconds(spawnCooldown);

        GameObject newAI = Instantiate(aiPrefab, spawnLocation.position, new Quaternion(), spawnLocation.transform);

        newAI.GetComponent<BasicAI>().homeSpawner = this;

        AI.Add(newAI);

        crRunning = false;

    }
}
