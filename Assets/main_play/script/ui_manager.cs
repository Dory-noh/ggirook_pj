using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ui_manager : MonoBehaviour
{
    public GameObject window_img;
    public GameObject success_img;
    public GameObject fail_img;
    public Text coin_text;
    public int coin;
    public int exp;
    public int initExp = 15;
    public Image exp_bar;
    public int level;
    public Text level_text;
    public int count; //Á×ÀÎ ÀûÀÇ ¼ö
    public Text count_text;
    
    // Start is called before the first frame update
    void Start()
    {
        
        window_img.SetActive(false);
        success_img.SetActive(false);
        fail_img.SetActive(false);
        coin = 130;
        exp = 0;
        level = 1;
        exp_bar = GameObject.FindGameObjectWithTag("exp_bar").GetComponent<Image>();
        level_text = GameObject.FindGameObjectWithTag("LEVELTXT").GetComponent<Text>();
        level_text.text = level.ToString();
        exp_bar.fillAmount = exp;
        count = 0;
        count_text = GameObject.FindGameObjectWithTag("count_txt").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        coin_text.text = coin.ToString();
        
    }
    public void speed_slow()
    {
        Time.timeScale = 0.5f;
    }
    public void speed_fast()
    {
        Time.timeScale = 2f;
    }
    public void open_window()
    {
        window_img.SetActive(true);
        Time.timeScale = 0f;
    }
    public void close_window()
    {
        window_img.SetActive(false);
        Time.timeScale = 1f;
    }
    public void open_success()
    {
        Time.timeScale = 0;
        success_img.SetActive(true);
    }
    public void close_success()
    {
        success_img.SetActive(false);
    }
    public void open_fail()
    {
        Time.timeScale = 0;
        fail_img.SetActive(true);
    }
    public void close_fail()
    {
        fail_img.SetActive(false);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Backtohome()
    {
        SceneManager.LoadScene(0);
    }
}
