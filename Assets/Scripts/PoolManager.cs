using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //프리팹들을 보관 할 리스트
    public GameObject[] Enemies;
    //풀 담당을 할 리스트
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
        // 선택한 풀의 놀고 있는 오브젝트에 접근
        //발견이 되었다면 select에 할당
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
        //못찾았다면 새로운 오브젝트 생성 후 풀에 삽입
        if (select == null)
        {
            select = Instantiate(Enemies[index], transform);
            pool[index].Add(select);
        }
        return select;
    }
}
