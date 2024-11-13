using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_manager : MonoBehaviour
{
    public GameObject gull_prefab;
    public GameObject ui_manager;
    int coin;
    public float time;
    bool[] is_respawn = { true, true, true, true, true };
    public int gull_num;
    // Start is called before the first frame update
    void Start()
    {
        ui_manager = GameObject.FindGameObjectWithTag("ui_manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (gull_prefab.transform.CompareTag("gull_b1"))
        {
            time = 0.4f;
        }
        else if (gull_prefab.transform.CompareTag("gull_b2"))
        {
            time = 0.6f;
        }
        else if (gull_prefab.transform.CompareTag("gull_b3"))
        {
            time = 1f;
        }
        else if (gull_prefab.transform.CompareTag("gull_b4"))
        {
            time = 1.6f;
        }
        else if (gull_prefab.transform.CompareTag("gull_b5"))
        {
            time = 3f;
        }
    }

    IEnumerator respawn_gull()
    {
        int y = Random.Range(-1, 2);
        yield return new WaitForSeconds(time);
        GameObject gull = Instantiate(gull_prefab);
        gull.transform.position = new Vector3(-10.55f, y, 0.35f);
        is_respawn[gull_num] = true;
    }
    public void respawn_gull_btn(int price)
    {
        int coin = ui_manager.GetComponent<ui_manager>().coin;
        int temp = coin - price;
        if (temp >= 0 && is_respawn[gull_num])
        {
            is_respawn[gull_num] = false;
            ui_manager.GetComponent<ui_manager>().coin = temp;
            StartCoroutine(respawn_gull());
        }

        
    }
}
