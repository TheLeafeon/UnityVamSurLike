using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    // 프리팹을 보관 할 변수 필요
    // 풀 담당을 하는 리스트들이 필요하다.
    // 프리팹의 개수와 리스트의 개수는 1:1 비율이다.

    public GameObject[] prefabs;

    private List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject> ();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 놀고 있는(비활성화 된) 게임오브젝트 접근
        // 발견하면, select 변수에 할당
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)//비활성화 상태라면
            {
                select = item;
                select.SetActive(true);

                break;
            }
        }

        if(select == null) // 못찾았다면, 새롭게 생성해서 select변수에 할당
        {
            select = Instantiate(prefabs[index] , transform);

            pools[index].Add(select);
        }



        return select;
    }
}
