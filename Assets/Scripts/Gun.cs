using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    [SerializeField]
    private bool cheat = true;

    private Ray GunRay => new Ray(transform.position, transform.forward);
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool Shoot()
    {
        audioSource.Play();

        return (cheat && Physics.SphereCast(GunRay, 15f)) || Physics.Raycast(GunRay);
    }
}
