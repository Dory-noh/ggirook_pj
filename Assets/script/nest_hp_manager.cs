using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nest_hp_manager : MonoBehaviour
{
    public Image hp_img;
    public int hp = 0;
    bool gameover = false;
    public int Maxhp = 150;
    // Start is called before the first frame update
    void Start()
    {
        hp_img = GameObject.FindGameObjectWithTag("nest_hp").GetComponent<Image>();
        hp = Maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        hp_img.fillAmount = (float)hp/(float)Maxhp;
        if (hp < 0 && gameover == false)
        {
            GameObject.FindGameObjectWithTag("ui_manager").GetComponent<ui_manager>().open_fail();
            gameover = true;

        }
    }
}
