using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private PlayerControls controls;

    public void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Shoot.performed += context => SceneManager.LoadScene(1);

        var distributor = AudioDistributor.instance;
        if(distributor)
            Destroy(distributor.gameObject);
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
