using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class that spawns and removes the pipes at a set interval.
/// </summary>
public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 1.5f;
    [SerializeField] private GameObject pipePrefab;

    private int lastPipeIndex;
    private List<GameObject> pipes = new List<GameObject>();
    public float height;

    private float timer;

    private void Start()
    {
        lastPipeIndex = 0;
        SpawnPipe();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            SpawnPipe();
            timer = 0;
        }

        RemoveOffscreenPipes();
    }

    private void SpawnPipe()
    {
        //height = Random.Range(-1.3f, 2.8f);
        height = Random.Range(-0.95f, 2f);
        // float height = Random.Range(-0.2f, 0.2f);
        Vector3 position = new Vector3(transform.position.x, height, transform.position.z);
        GameObject newPipe = Instantiate(pipePrefab, position, Quaternion.identity, transform);
        pipes.Add(newPipe);
    }

    private void RemoveOffscreenPipes()
    {
        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            if (pipes[i].transform.position.x < -10)
            {
                Destroy(pipes[i]);
                pipes.RemoveAt(i);
                lastPipeIndex -= 1;
            }
        }
    }

    public void ResetPipes()
    {
        lastPipeIndex = 0;

        foreach (var pipe in pipes)
        {
            Destroy(pipe);
        }
        pipes.Clear();
        timer = 0;
    }

    public GameObject GetNextPipe(){
        if(pipes.Count == 0 || lastPipeIndex >= pipes.Count){
            return null;
        }

        GameObject nextPipe = pipes[lastPipeIndex];
        return nextPipe;

    }

    public void increaseLastPipeIndex(){
        lastPipeIndex++;
    }
}