using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_manager : MonoBehaviour
{
    public GameObject main_camera;
    public GameObject left_btn;
    public GameObject right_btn;
    // Start is called before the first frame update
    void Start()
    {
        main_camera = GameObject.FindGameObjectWithTag("MainCamera");
        left_btn = GameObject.FindGameObjectWithTag("left_btn");
        right_btn = GameObject.FindGameObjectWithTag("right_btn");
        main_camera.transform.position = new Vector3(-1, 2f, -20f);
        left_btn.SetActive(false);
        right_btn.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void move_camera_left()
    {
        main_camera.transform.position = new Vector3(-1, 2f,-20f);
        left_btn.SetActive(false);
        right_btn.SetActive(true);
    }
    public void move_camera_right()
    {
        main_camera.transform.position = new Vector3(17.51f, 1.67f, -11.45f);
        left_btn.SetActive(true);
        right_btn.SetActive(false);
    }
}
