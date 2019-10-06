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

        if(!won)
        {
            audioSource.clip = loseClip;
            audioSource.Play();
            yield return new WaitForSeconds(loseClip.length);
            AudioDistributor.instance.DecrementGroup();
        }

        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex);

        IsRunning = false;
    }
}
