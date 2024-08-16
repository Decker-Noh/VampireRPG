using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;

    [Header("# Game Info")]
    public float currentGameTime;
    public float maxGameTime = 50f;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 270, 340, 420, 510 };
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        currentGameTime += Time.deltaTime;
        if (currentGameTime > maxGameTime)
        {
            currentGameTime = maxGameTime;
        }
    }

    public void GetEXP()
    {
        exp++;
        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }

}
