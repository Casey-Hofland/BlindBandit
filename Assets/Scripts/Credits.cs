using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private PlayerControls controls;
    private Gun gun;

    private Coroutine tryQuit = null;
    private bool canQuit = false;

    private void Awake()
    {
        gun = GetComponentInChildren<Gun>();
        controls = new PlayerControls();

        controls.Gameplay.Shoot.performed += context =>
        {
            if (tryQuit == null)
            {
                tryQuit = StartCoroutine(TryQuit());
            }
        };
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(32);
        canQuit = true;
    }

    private IEnumerator TryQuit()
    {
        gun.Shoot();

        if(canQuit)
        {
            yield return new WaitForSeconds(gun.GetComponent<AudioSource>().clip.length);
            Application.Quit();
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
