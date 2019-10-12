using UnityEngine;

public class Input : MonoBehaviour
{
    public PlayerControls controls { get; private set; }
    private Movement movement;
    private Gun gun;
    private SpawnEnemy spawnEnemy;
    private HandleWinState handleWinState;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        gun = GetComponentInChildren<Gun>();
        spawnEnemy = GetComponent<SpawnEnemy>();
        handleWinState = GetComponent<HandleWinState>();

        // Configure Controls
        controls = new PlayerControls();
        controls.Gameplay.Shoot.performed += context =>
        {
            foreach(var source in FindObjectsOfType<AudioSource>())
                source.Stop();

            spawnEnemy.StopAllCoroutines();
            spawnEnemy.enabled = false;

            if (!handleWinState.IsRunning)
                StartCoroutine(handleWinState.Do(gun.Shoot()));
        };
        controls.Gameplay.Move.performed += context => Move(context.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Move(Vector2 input)
    {
        movement.Move(input.y);
        movement.Rotate(input.x);
    }
}
