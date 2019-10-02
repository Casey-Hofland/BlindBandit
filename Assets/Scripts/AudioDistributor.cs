using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

public class AudioDistributor : MonoBehaviour
{
    [SerializeField]
    private LevelOfRandomness levelOfRandomness = LevelOfRandomness.PlaySequential;
    [SerializeField]
    private ClipGroup[] clipGroups = new ClipGroup[1];
    
    private int currentGroup = -1;

    public static AudioDistributor instance = null;

    private enum LevelOfRandomness
    {
        PlaySequential,
        RandomizeGroups,
        RandomizeAll,
    }

    [Serializable]
    private class ClipGroup
    {
        [SerializeField]
        private AudioClip gunClip = null;
        [SerializeField]
        private AudioClip enemyClip = null;
        [SerializeField]
        private AudioClip musicClip = null;
        [SerializeField]
        private Vector2 spawnDelayOverride = Vector2.zero;
        [SerializeField]
        private AudioClip[] distractionClips = new AudioClip[1];

        public float RandomSpawnDelayOverride => (spawnDelayOverride == Vector2.zero)
            ? -1f
            : Random.Range(spawnDelayOverride.x, spawnDelayOverride.y);

        public AudioClip GetClip(ClipType clipType)
        {
            switch(clipType)
            {
                case ClipType.Gun:
                    return gunClip;
                case ClipType.Enemy:
                    return enemyClip;
                case ClipType.Music:
                    return musicClip;
                case ClipType.Distraction:
                    int index = Random.Range(0, distractionClips.Length);
                    return distractionClips[index];
                default:
                    return null;
            }
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += (s, lsm) => IncrementGroup();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= (s, lsm) => IncrementGroup();
    }

    public AudioClip GetSoundClip(ClipType clipType)
    {
        Assert.IsTrue(clipGroups.Length > 0);

        switch(levelOfRandomness)
        {
            case LevelOfRandomness.PlaySequential:
            case LevelOfRandomness.RandomizeGroups:
                return clipGroups[currentGroup].GetClip(clipType);
            case LevelOfRandomness.RandomizeAll:
                int index = Random.Range(0, clipGroups.Length);
                return clipGroups[index].GetClip(clipType);
            default:
                return null;
        }
    }

    public void IncrementGroup()
    {
        switch(levelOfRandomness)
        {
            case LevelOfRandomness.PlaySequential:
                currentGroup++;
                if(currentGroup >= clipGroups.Length)
                    currentGroup = 0;
                break;
            case LevelOfRandomness.RandomizeGroups:
                currentGroup = Random.Range(0, clipGroups.Length);
                break;
            default:
                return;
        }

        float spawnDelayOverride = clipGroups[currentGroup].RandomSpawnDelayOverride;
        if (!Mathf.Approximately(spawnDelayOverride, -1f))
        {
            FindObjectOfType<SpawnEnemy>().SetSpawnDelay(spawnDelayOverride);
        }
    }
}
