using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    // �������� ���� �� ���� �ʿ�
    // Ǯ ����� �ϴ� ����Ʈ���� �ʿ��ϴ�.
    // �������� ������ ����Ʈ�� ������ 1:1 �����̴�.

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

        // ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����
        // �߰��ϸ�, select ������ �Ҵ�
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)//��Ȱ��ȭ ���¶��
            {
                select = item;
                select.SetActive(true);

                break;
            }
        }

        if(select == null) // ��ã�Ҵٸ�, ���Ӱ� �����ؼ� select������ �Ҵ�
        {
            select = Instantiate(prefabs[index] , transform);

            pools[index].Add(select);
        }



        return select;
    }
}
