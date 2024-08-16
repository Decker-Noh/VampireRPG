using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabID;
    public float damage;
    public int count;
    public float speed;

    float timer;

    Player player;
    private void Awake()
    {
        player = GameManager.Instance.player;
    }
    public void Init(ItemData data)
    {

        //Base Set
        name = "Weapon : " + data.itemName;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        id = data.itemID;
        damage = data.baseDamage;
        count = data.baseCount;
        for (int i=0; i< GameManager.Instance.pool.Enemies.Length; i++)
        {
            if (data.projectile == GameManager.Instance.pool.Enemies[i])
            {
                prefabID = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            case 1:
                speed = 0.3f;
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed *  Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
            default:
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            LevelUP(damage + 5, count + 1);
        }
    }
    public void LevelUP(float damage, int count)
    {
        this.damage = damage;
        this.count = count;
        if (id == 0)
            Batch();
    }
    void Batch()
    {
        for (int i=0; i<count; i++)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.Instance.pool.Get(prefabID).transform;
            }
            bullet.parent = transform;
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localRotation = Quaternion.identity;
            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity per
        }
    }
    void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }
        Vector3 targetPos = player.scanner.nearestTarget.transform.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        
        Transform bullet = GameManager.Instance.pool.Get(prefabID).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

    }
}
