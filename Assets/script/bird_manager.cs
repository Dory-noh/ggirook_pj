using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_manager : MonoBehaviour
{
    public GameObject gull_prefab;
    public GameObject ui_manager;
    int coin;
    // Start is called before the first frame update
    void Start()
    {
        ui_manager = GameObject.FindGameObjectWithTag("ui_manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator respawn_gull()
    {
        int y = Random.Range(-1, 2);
        float time = 0.1f;
        yield return new WaitForSeconds(time);
        GameObject gull = Instantiate(gull_prefab);
        gull.transform.position = new Vector3(-10.55f, y, 0.35f);
    }
    public void respawn_gull_btn(int price)
    {
        int coin = ui_manager.GetComponent<ui_manager>().coin;
        int temp = coin - price;
        if (temp >= 0)
        {
            ui_manager.GetComponent<ui_manager>().coin = temp;
            StartCoroutine(respawn_gull());
        }

        
    }
}
