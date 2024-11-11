using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gull : MonoBehaviour
{
    public Image hp_img;
    bool ismove;
    float hp;
    int maxhp;
    int power;
    float speed;
    public int respawn_time;//소환까지 걸리는 시간
    // Start is called before the first frame update
    void Start()
    {
        if (transform.CompareTag("gull_b1"))
        {
            maxhp = 50;
            power = 10;
            speed = 0.1f;
        }
        if (transform.CompareTag("gull_b2"))
        {
            maxhp = 150;
            power = 20;
            speed = 0.2f;
        }
        if (transform.CompareTag("gull_b3"))
        {
            maxhp = 200;
            power = 10;
            speed = 0.4f;
        }
        if (transform.CompareTag("gull_b4"))
        {
            maxhp = 40;
            power = 100;
            speed = 0.2f;
        }
        if (transform.CompareTag("gull_b5"))
        {
            maxhp = 60;
            power = 60;
            speed = 0.2f;
        }
        hp = maxhp;
        ismove = true;
        StartCoroutine(move());
        hp_img = gameObject.transform.GetComponentInChildren<Canvas>().transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hp_img.fillAmount = (float)hp / (float)maxhp;
    }

    IEnumerator move()
    {
        while (true)
        {
            yield return new WaitForSeconds(speed);
            if (ismove)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(26.44f, transform.position.y, 0.35f), 0.01f * 2f);
                //if(Vector3.Distance(gull.transform.position,))
            }
            else
            {

            }

        }
    }
    public void hit(int power)
    {
        StartCoroutine(damage(power)); //gull의 데미지를 깎는 함수
        if (hp < 0) Destroy(gameObject);
    }

    IEnumerator damage(int power)
    {
        yield return new WaitForSeconds(0.1f);
        hp -= power;
        transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.3f);
        ismove = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag.StartsWith("enemy"))
        {
            ismove = false;
            other.transform.GetComponent<devil>().hit();
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }

        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
