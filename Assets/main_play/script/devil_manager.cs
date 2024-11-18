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
    string enemy_name = "";
    bool respawn_gull;
    bool respawn_fox;
    bool respawn_pelican;
    // Start is called before the first frame update
    void Start()
    {
        nest = GameObject.FindGameObjectWithTag("nest");
        respawn_fox = true;
        respawn_gull = true;
        respawn_pelican = true;

    }

    // Update is called once per frame
    void Update()
    {
        day = GameObject.FindGameObjectWithTag("ani_manager").GetComponent<ani_manager>().day;
        if(day == 1 && devil_prefab.CompareTag("enemy_gull") && respawn_gull)
        {
            enemy_name = "enemy_gull";
            respawn_devil_repeat();
            time = 3;
            respawn_gull = false;
        }
        if(day == 3 && devil_prefab.CompareTag("enemy_fox") && respawn_fox)
        {
            enemy_name = "enemy_fox";
            respawn_devil_repeat();
            time = 20;
            respawn_fox = false;
        }
        if (day == 8 && devil_prefab.CompareTag("enemy_pelican") && respawn_pelican)
        {
            enemy_name = "enemy_pelican";
            respawn_devil_repeat();
            time = 30;
            respawn_pelican = false;
        }
    }

    IEnumerator respawn_devil()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {

            float y = Random.Range(-0.5f, 2);
            GameObject devil = Instantiate(devil_prefab);
            devil.transform.name = enemy_name;
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
