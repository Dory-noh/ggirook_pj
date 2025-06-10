using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolingManager : MonoBehaviour
{
    public static poolingManager Instance;
    //풀링할 프리팹
    public GameObject[] prefabs;
    //풀링한 오브젝트 모아놓을 오브젝트
    private GameObject PoolingParent;
    //초기 생성할 오브젝트 수
    public int[] poolSize = {30, 10, 250};
    //풀에 저장될 오브젝트 리스트
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
            //갈매기만 초기 생성 개수 30, 나머지 10
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
        obj.SetActive(false);//오브젝트 비활성화
    }
}
