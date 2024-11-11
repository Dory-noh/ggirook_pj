using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class devil_manager : MonoBehaviour
{
    public GameObject devil_prefab;
    float time;
    float speed;
    public GameObject nest;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        nest = GameObject.FindGameObjectWithTag("nest");
       
       respawn_devil_repeat();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator respawn_devil()
    {
        time = 3;
        yield return new WaitForSeconds(2f);
        while (true)
        {
            
            int y = Random.Range(-3, 3);
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
