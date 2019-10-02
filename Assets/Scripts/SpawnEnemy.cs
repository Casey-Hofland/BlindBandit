using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy = null;
    [SerializeField]
    private Vector2 distanceRange = new Vector2(10f, 30f);
    [SerializeField]
    private Vector2 spawnDelayRange = new Vector2(5f, 30f);

    private float RandomDistance => Random.Range(distanceRange.x, distanceRange.y);
    private float RandomSpawnDelay => Random.Range(spawnDelayRange.x, spawnDelayRange.y);

    private float spawnDelay = 0f;

    private void Awake()
    {
        spawnDelay = RandomSpawnDelay;
    }

    public void SetSpawnDelay(float spawnDelay)
    {
        this.spawnDelay = spawnDelay;
    }

    IEnumerator Start()
    {
        Vector2 randomSpawnPosition = Random.insideUnitCircle * RandomDistance;
        Vector3 spawnPosition = new Vector3(randomSpawnPosition.x, transform.position.y, randomSpawnPosition.y);
        yield return new WaitForSeconds(spawnDelay);

        GameObject spawn = Instantiate(enemy, spawnPosition, Quaternion.identity);
        spawn.transform.LookAt(transform);
        AudioSource audioSource = spawn.GetComponentInChildren<AudioSource>();
        audioSource.clip = AudioDistributor.instance.GetSoundClip(ClipType.Enemy);
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        HandleWinState handleWinState = GetComponent<HandleWinState>();
        if(!handleWinState.IsRunning)
            StartCoroutine(handleWinState.Do(false));
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, distanceRange.y);
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, distanceRange.x);
    }
#endif
}