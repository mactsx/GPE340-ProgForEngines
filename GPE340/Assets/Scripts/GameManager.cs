using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject PlayerControllerPrefab;
    public GameObject AIControllerPrefab;
    public GameObject PlayerPawnPrefab;
    public GameObject EnemyPawnPrefab;
    public GameObject cameraPrefab;

    [Header("Other")]
    public static GameManager instance;
    public CameraController mainCamera;
    public PlayerController player;

    [Header("Spawning")]
    // Places pawns can spawn from
    public SpawnPoint[] spawnPoints;
    public List<AIController> aiEnemies;

    [Header("Waves")]
    public List<Wave> waves;
    public int currentWave;
    public int enemiesLeft;
    public bool isPaused;

    public void Awake()
    {
        // Make a singleton
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        FindCamera();
        FindSpawnPoints();
        SpawnPlayer();
        currentWave = 0;
        SpawnWave(currentWave);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Find main camera in scene
    public void FindCamera()
    {
        mainCamera = FindObjectOfType<CameraController>();
    }

    public void DisconnectCameraFromPlayer()
    {
        mainCamera.transform.SetParent(null);
    }

    // Load all spawnpoints from the scene into the array
    public void FindSpawnPoints()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    // Get a random spawn point
    public Transform GetRandomSpawn()
    {
        // Check there are spawn points
        if (spawnPoints.Length > 0)
        {
            // Return a random point
            return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        }
        // Otherwise there is nothing to return
        return null;
    }

    // Spawn the player
    public void SpawnPlayer()
    {
        // Spawn the player controller
        GameObject controllerObj = Instantiate(PlayerControllerPrefab, Vector3.zero, Quaternion.identity);
        player = controllerObj.GetComponent<PlayerController>();

        // Connect the pawn and controller
        SpawnPlayerPawn();

    }

    public void SpawnPlayerPawn()
    {
        Pawn tempPawn = SpawnPawn();

        player.PossessPawn(tempPawn);

        // Assign the camera
        AssignCamera(tempPawn);
    }

    // Spawn default player pawn
    public Pawn SpawnPawn()
    {
        return SpawnPawn(PlayerPawnPrefab);
    }

    // Spawn a specific gameobject pawn
    public Pawn SpawnPawn(GameObject pawnToSpawn)
    {
        // Get random spawn and spawn a pawn
        Transform randomSpawn = GetRandomSpawn();
        GameObject PawnObj = Instantiate(pawnToSpawn, randomSpawn.transform.position, randomSpawn.transform.rotation);
        Pawn tempPawn = PawnObj.GetComponent<Pawn>();

        return tempPawn;
    }

    // Spawn default enemy
    public void SpawnEnemy ()
    {
        SpawnEnemy(EnemyPawnPrefab);
    }

    // Spawn specific enemy
    public void SpawnEnemy(GameObject pawnToSpawn)
    {
        // Create controller at 0
        GameObject AIConObj = Instantiate(AIControllerPrefab, Vector3.zero, Quaternion.identity);
        AIController AICon = AIConObj.GetComponent<AIController>();

        // Add it to list of enemies
        aiEnemies.Add(AICon);

        // Connect and spawn pawn
        AICon.PossessPawn(SpawnPawn(pawnToSpawn));

        // Add function when enemy dies
        Health AIHealth = AICon.pawn.GetComponent<Health>();
        if (AIHealth != null)
        {
            AIHealth.OnDie.AddListener(OnEnemyDeath);
            Debug.Log("Added event");
        }
    }

    public void AssignCamera(Pawn tempPawn)
    {
        // See if there is a camera in the scene
        // If not, create one and set it to be above the pawn
        if (mainCamera == null)
        {
            GameObject cameraObj = Instantiate(cameraPrefab);
            mainCamera = cameraObj.GetComponent<CameraController>();
        }

        // Set the pawn to be a parent of the camera
        mainCamera.transform.SetParent(tempPawn.transform);
        // Set the camera target
        mainCamera.target = tempPawn.transform;
        // Make sure the camera is in the right location
        Vector3 cameraOffset = new Vector3(0f, 5f, -1f);
        mainCamera.transform.position = tempPawn.transform.position + cameraOffset;
        mainCamera.transform.rotation = tempPawn.transform.rotation;

        // Subscribe to the on die event - add a way to disconnect the parent/child relationship of the camera
        // so the camera does not get destroyed when the player dies
        Health playerHealth = tempPawn.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.OnDie.AddListener(DisconnectCameraFromPlayer);
            playerHealth.OnDie.AddListener(OnPlayerDeath);
        }
    }

    public void WhenGameOver()
    {
        Debug.Log("GAME OVER!");
    }

    public void WhenWin()
    {
        Debug.Log("*********** All Enemies Killed - VICTORY ***********");
    }

    // For adding respawn button later - do not assign anywhere
    public void RespawnPlayer()
    {

        // Check if there are enough lives
        if (player.lives > 0)
        {
            // destroy current pawn
            Destroy(player.pawn.gameObject);

            // Unpossess pawn
            player.UnpossessPawn();

            // Spawn new pawn
            SpawnPlayerPawn();

            // Subtract a life
            player.lives--;
        }
        // Otherwise they are out of lives and game over
        else
        {
            WhenGameOver();
        }
    }

    public void OnPlayerDeath()
    {
        RespawnPlayer();
    }

    public void OnEnemyDeath()
    {
        // Subtract that enemy from the total
        enemiesLeft--;
        

        Debug.Log(enemiesLeft + " enemies left");

        // If that was the last enemy, spawn a new wave
        if (enemiesLeft <= 0)
        {
            currentWave++;

            // If there is data for this wave number
            if (currentWave < waves.Count)
            {
                // Spawn the wave
                SpawnWave(currentWave);
            }
            // Otherwise there are no more waves and the player wins
            else
            {
                WhenWin();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
    public void TogglePause()
    {
        if (isPaused)
            Unpause();
        else
            Pause();
    }

    public void SpawnWave(int waveNum)
    {
        SpawnWave(waves[waveNum]);
    }

    public void SpawnWave (Wave wave)
    {
        // Make sure the list of enemies is clear
        ClearWave(); 

        // Spawn each enemy in the wave
        foreach (Pawn enemy in wave.enemies)
        {
            GameObject pawnObj = enemy.gameObject;
            SpawnEnemy(pawnObj);
        }
        // How many enemies are left
        enemiesLeft = wave.enemies.Count;
    }

    public void ClearWave()
    {
        // For each enemy in the list
        foreach (AIController enemy in aiEnemies)
        {
            // If the enemy exists
            if (enemy != null)
            {
                // If it has a pawn
                if(enemy.pawn != null)
                {
                    // Destroy the pawn
                    Destroy(enemy.pawn.gameObject);
                }
                // Destroy the controller
                Destroy(enemy.gameObject);
            }
        }
        // Clear the list
        aiEnemies.Clear();
    }


}
