using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class devil_manager : MonoBehaviour
{
    public GameObject devil_prefab;
    float time;
    float speed;
    public GameObject nest;
    int day;
    bool respawn_gull;
    bool respawn_fox;
    // Start is called before the first frame update
    void Start()
    {
        nest = GameObject.FindGameObjectWithTag("nest");
        respawn_fox = true;
        respawn_gull = true;
    }

    // Update is called once per frame
    void Update()
    {
        day = GameObject.FindGameObjectWithTag("ani_manager").GetComponent<ani_manager>().day;
        if(day == 1 && devil_prefab.CompareTag("enemy_gull") && respawn_gull)
        {
            respawn_devil_repeat();
            respawn_gull = false;
        }
        if(day == 5 && devil_prefab.CompareTag("enemy_fox") && respawn_fox)
        {
            respawn_devil_repeat();
            respawn_fox = false;
        }
    }

    IEnumerator respawn_devil()
    {
        time = 3;
        yield return new WaitForSeconds(2f);
        while (true)
        {
            
            int y = Random.Range(-1, 2);
            GameObject devil = Instantiate(devil_prefab);
            devil.transform.position = new Vector3(31, y, 0.35f);
            yield return new WaitForSeconds(time);
            if(time > 0.5f) time -= 0.1f;
        }

        
        
        
    }
    void respawn_devil_repeat()
    {
        StartCoroutine(respawn_devil());
    }

    
}
