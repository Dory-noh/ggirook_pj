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
    public float exp;
    public Image exp_bar;
    // Start is called before the first frame update
    void Start()
    {
        window_img.SetActive(false);
        success_img.SetActive(false);
        fail_img.SetActive(false);
        coin = 1000;
        exp = 0;
        exp_bar = GameObject.FindGameObjectWithTag("exp_bar").GetComponent<Image>();
        exp_bar.fillAmount = exp;
        
    }

    // Update is called once per frame
    void Update()
    {
        coin_text.text = coin.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            close_fail();
        }
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
    public void QuitGame()
    {
        Application.Quit();
    }
}
