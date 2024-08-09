using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public PoolManager pool;

    public float currentGameTime;
    public float maxGameTime = 50f;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        currentGameTime += Time.deltaTime;
        if (currentGameTime > maxGameTime)
        {
            currentGameTime = maxGameTime;
        }
    }

}
