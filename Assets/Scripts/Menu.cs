using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private PlayerControls controls;
    private Gun gun;

    public void Awake()
    {
        gun = GetComponentInChildren<Gun>();

        controls = new PlayerControls();
        controls.Gameplay.Shoot.performed += context => SceneManager.LoadScene(1);
        controls.Gameplay.Shoot.performed += context => gun.Shoot();

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
