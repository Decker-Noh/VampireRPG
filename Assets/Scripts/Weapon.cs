using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabID;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
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
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Infinity per
        }
    }
}
