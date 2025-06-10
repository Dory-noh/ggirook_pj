using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolingManager : MonoBehaviour
{
    public static poolingManager Instance;
    //Ǯ���� ������
    public GameObject[] prefabs;
    //Ǯ���� ������Ʈ ��Ƴ��� ������Ʈ
    private GameObject PoolingParent;
    //�ʱ� ������ ������Ʈ ��
    public int[] poolSize = {30, 10, 250};
    //Ǯ�� ����� ������Ʈ ����Ʈ
    private List<List<GameObject>> pooledObjects;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
            PoolingParent = gameObject;
        pooledObjects = new List<List<GameObject>>();
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject objParent = new GameObject(prefabs[i].name);
            List<GameObject> tempList = new List<GameObject>();
            //���ű⸸ �ʱ� ���� ���� 30, ������ 10
            for(int j = 0; j < poolSize[i<6?0:i==9?2:1]; j++)
            {
                GameObject obj = Instantiate(prefabs[i]);
                obj.SetActive(false);
                obj.transform.parent = objParent.transform;
                tempList.Add(obj);
            }
            objParent.transform.parent =  PoolingParent.transform;
            pooledObjects.Add(tempList);
        }
    }


    public GameObject GetPooledObj(int idx)
    {
        foreach (GameObject obj in pooledObjects[idx])
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }

    public void ReturnPooledObj(GameObject obj)
    {
        obj.SetActive(false);//������Ʈ ��Ȱ��ȭ
    }
}
