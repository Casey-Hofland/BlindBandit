using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class HandleWinState : MonoBehaviour
{
    [SerializeField]
    private AudioClip winClip = null;
    [SerializeField]
    private AudioClip loseClip = null;
    [SerializeField]
    private float suspenseTime = 3f;

    public bool IsRunning { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator Do(bool won)
    {
        IsRunning = true;

        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(suspenseTime);
        Time.timeScale = 1;

        audioSource.clip = (won) ? winClip : loseClip;
        audioSource.Play();

        yield return new WaitUntil(() => GetComponent<Input>().controls.Gameplay.Shoot.triggered);

        int buildIndex = (won) ? SceneManager.GetActiveScene().buildIndex : 0;
        SceneManager.LoadScene(buildIndex);

        IsRunning = false;
    }
}
