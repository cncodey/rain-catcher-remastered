using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.VFX;

public class Dropper : MonoBehaviour
{
    public GameObject lnBolt;
    public GameObject waterDrop;
    public GameObject snowflake;

    public int lnBoltChance;
    public int snowflakeChance;

    public GameManager manager;
    private float dropTimer = 0.75f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        dropTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!manager.isGameOver && !manager.isGamePaused && dropTimer <= 0f)
        {
            int spawnChance = UnityEngine.Random.Range(1, 100);
            GameObject dropped;
            if (spawnChance < lnBoltChance)
            {
                dropped = lnBolt;
            }
            else if (spawnChance < snowflakeChance + lnBoltChance)
            {
                dropped = snowflake;
            }
            else
            {
                dropped = waterDrop;
            }
            Instantiate(dropped, transform.position + new Vector3(UnityEngine.Random.Range(-8f, 8f), 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
            dropTimer = 0.75f;
        }
    }
}
