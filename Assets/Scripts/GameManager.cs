using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static bool IsPlayerDead { get; set; } = false;
    public static event Action Restart = delegate { };
    public static event Action NextFloor = delegate { };
    private static FloorSpawner[] spawners;

    void Awake()
    {
        Restart += Reset;
        NextFloor += Spawn;
    }

    void Start()
    {
        spawners = FindObjectsOfType<FloorSpawner>();
        var spawnerIndex = UnityEngine.Random.Range(0, spawners.Length);
        spawners[spawnerIndex].Spawn();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Floor.CurrentFloor.Stop();
            if (IsPlayerDead)
            {
                Restart.Invoke();
            }
            else
            {
                NextFloor();
            }
        }
    }

    static void Spawn()
    {
        var spawnerIndex = UnityEngine.Random.Range(0, spawners.Length);
        spawners[spawnerIndex].Spawn();
    }

    public static void Reset()
    {
        IsPlayerDead = false;
        spawners = null;
        Restart -= Reset;
        NextFloor -= Spawn;
        SceneManager.LoadScene(0);
    }
}
