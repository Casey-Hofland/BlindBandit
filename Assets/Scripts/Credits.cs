using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private PlayerControls controls;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(32);
        controls = new PlayerControls();
        controls.Gameplay.Shoot.performed += context => Application.Quit();
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
