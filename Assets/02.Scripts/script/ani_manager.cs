using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ani_manager : MonoBehaviour
{
    public GameObject obj;
    public Text day_text;
    public int day;
    // Start is called before the first frame update
    void Start()
    {
        day = 0;
        day_text.text = "Day 1";
        StartCoroutine(sun_ani());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator sun_ani()
    {
        while (true)
        {
            if ((day%21 == 0&&day!=0) || day > 105)
            {
                day_text.text = $"{++day / 21}StageClear!";
                if (day > 105)
                {
                    GameObject.FindGameObjectWithTag("ui_manager").GetComponent<ui_manager>().open_success();
                    day_text.text = "¼º°ø~!";
                    break;
                }
            }
            else
            {
                obj.transform.GetComponent<Animation>().Play();
                day_text.text = "Day " + (++day%21).ToString()+" / 21";
                yield return new WaitForSeconds(20f);
                obj.transform.GetComponent<Animation>().Rewind();
            }
        }
    }
}
