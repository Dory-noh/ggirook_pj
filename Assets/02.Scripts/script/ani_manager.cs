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
        if(GameManager.instance.EndlessMode) day_text.text = "Day 1";
        else day_text.text = "Day 1 / 21";
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
            if (GameManager.instance.EndlessMode)
            {
                if ((day % 21 == 0 && day != 0) || day > 1000)
                {
                    obj.transform.GetComponent<Animation>().Play();
                    day_text.text = $"<color=#ffff00>{day++ / 21} StageClear!</color>";
                    yield return new WaitForSeconds(1f);
                    day_text.text = "Day " + (day).ToString();
                    yield return new WaitForSeconds(19f);
                    obj.transform.GetComponent<Animation>().Rewind();

                    if (day > 1000)
                    {
                        GameObject.FindGameObjectWithTag("ui_manager").GetComponent<ui_manager>().open_success();
                        day_text.text = "성공~!";
                        break;
                    }
                }
                else
                {
                    obj.transform.GetComponent<Animation>().Play();
                    day_text.text = "Day " + (++day).ToString();
                    yield return new WaitForSeconds(20f);
                    obj.transform.GetComponent<Animation>().Rewind();
                }
            }
            else
            {
                if (day > 20)
                {
                    GameObject.FindGameObjectWithTag("ui_manager").GetComponent<ui_manager>().open_success();
                    day_text.text = "성공~!";
                    break;
                }
                else
                {
                    obj.transform.GetComponent<Animation>().Play();
                    day_text.text = "Day " + (++day).ToString() + " / 21";
                    yield return new WaitForSeconds(20f);
                    obj.transform.GetComponent<Animation>().Rewind();
                }
            }
        }
    }
}
