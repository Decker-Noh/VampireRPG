using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //�����յ��� ���� �� ����Ʈ
    public GameObject[] Enemies;
    //Ǯ ����� �� ����Ʈ
    List<GameObject>[] pool;
    private void Awake()
    {
        pool = new List<GameObject>[Enemies.Length];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = new List<GameObject>();
        }
    }
    public GameObject Get(int index)
    {
        // ������ Ǯ�� ��� �ִ� ������Ʈ�� ����
        //�߰��� �Ǿ��ٸ� select�� �Ҵ�
        GameObject select = null;
        foreach (GameObject item in pool[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //��ã�Ҵٸ� ���ο� ������Ʈ ���� �� Ǯ�� ����
        if (select == null)
        {
            select = Instantiate(Enemies[index], transform);
            pool[index].Add(select);
        }
        return select;
    }
}
