using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poop_manager : MonoBehaviour
{
    int poop_num = 9;
    //public GameObject poop_prefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(respawn_poop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator respawn_poop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            GameObject poop = poolingManager.Instance.GetPooledObj(poop_num);
            poop.transform.SetParent(transform);
            poop.transform.localPosition = new Vector3(0,4.708f,0);
        }
    }
}
