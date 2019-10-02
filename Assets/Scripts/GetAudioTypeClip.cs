using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GetAudioTypeClip : MonoBehaviour
{
    [SerializeField]
    private ClipType clipType = ClipType.None;
    [SerializeField]
    private bool playOnStart = false;
    [SerializeField]
    private bool continuousLoad = false;

    private AudioDistributor distributor;
    private AudioSource audioSource;

    private void Start()
    {
        distributor = AudioDistributor.instance;
        if(!distributor)
            return;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = distributor.GetSoundClip(clipType);

        if(playOnStart)
            audioSource.Play();

        if(continuousLoad)
            StartCoroutine(LoadNew());
    }

    private IEnumerator LoadNew()
    {
        if (audioSource.clip != null)
            yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = distributor.GetSoundClip(clipType);

        if(playOnStart)
            audioSource.Play();

        StartCoroutine(LoadNew());
    }
}
