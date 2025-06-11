using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gull : MonoBehaviour
{
    public bool adelie_push;
    public Image hp_img;
    bool ismove;
    public float hp;
    int maxhp;
    int power;
    float speed;
    Vector3 velo = Vector3.zero;
    Vector3 targetPos;
    public int respawn_time;
    float origin_y;

    public Animation walkAni;

    // 공격 관련 상태
    bool isAttacking = false;
    GameObject currentTarget = null;

    private void OnEnable()
    {
        walkAni = GetComponent<Animation>();
        adelie_push = false;
        origin_y = transform.position.y;

        // 능력치 설정
        if (transform.CompareTag("gull_b1"))
        {
            maxhp = 50; power = 10; speed = 0.1f;
        }
        if (transform.CompareTag("gull_b2"))
        {
            maxhp = 150; power = 20; speed = 0.2f;
        }
        if (transform.CompareTag("gull_b3"))
        {
            maxhp = 200; power = 10; speed = 0.2f;
            adelie_push = true;
        }
        if (transform.CompareTag("gull_b4"))
        {
            maxhp = 400; power = 20; speed = 0.2f;
        }
        if (transform.CompareTag("gull_b5"))
        {
            maxhp = 150; power = 250; speed = 0.2f;
        }

        hp = maxhp;
        ismove = true;
        StartCoroutine(move());

        transform.GetChild(2).gameObject.SetActive(false);
        hp_img = gameObject.transform.GetComponentInChildren<Canvas>().transform.GetChild(1).GetComponent<Image>();

        if (transform.CompareTag("gull_b3"))
            targetPos = new Vector3(50.44f, origin_y, 0.35f);
        else
            targetPos = new Vector3(45.44f, origin_y, 0.35f);
    }

    IEnumerator move()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.06f);

            if (ismove)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velo, speed * 1200f * Time.deltaTime);
                if (!walkAni.isPlaying) walkAni.Play();

                if ((targetPos.x - transform.position.x) < 2.0f)
                {
                    StartCoroutine(DelayedReturn());
                }
            }
            else
            {
                if (walkAni.isPlaying) walkAni.Stop();
            }
        }
    }

    public void hit(int power)
    {
        if (gameObject.activeSelf)
            StartCoroutine(damage(power));
    }

    IEnumerator damage(int power)
    {
        yield return new WaitForSeconds(0.1f);
        hp -= power;
        hp = Mathf.Clamp(hp, 0, maxhp);

        if (hp <= 0)
        {
            StartCoroutine(DelayedReturn());
        }

        hp_img.fillAmount = hp / maxhp;
        if (hp_img.fillAmount <= 0.3f)
            hp_img.color = Color.red;
        else if (hp_img.fillAmount <= 0.5f)
            hp_img.color = Color.yellow;
        else
            hp_img.color = Color.green;

        // 피격 연출 (밀리는 느낌)
        transform.position = new Vector3(transform.position.x - 0.23f, origin_y, transform.position.z);
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(transform.position.x + 0.2f, origin_y, transform.position.z);
        yield return new WaitForSeconds(0.3f);

        ismove = true;
    }

    IEnumerator DelayedReturn()
    {
        yield return new WaitForSeconds(0.01f);
        poolingManager.Instance.ReturnPooledObj(gameObject);
        hp = maxhp;
        hp_img.fillAmount = 1.0f;
        hp_img.color = Color.green;
        isAttacking = false;
        ismove = true;
        currentTarget = null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.StartsWith("enemy"))
        {
            ismove = false;
            isAttacking = true;
            currentTarget = col.gameObject;
            col.GetComponent<devil>().hit(power); // 첫 타격

            if (!transform.CompareTag("gull_b3"))
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
            }

            StartCoroutine(AttackLoop());
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == currentTarget)
        {
            isAttacking = false;
            ismove = true;
            currentTarget = null;
        }
    }

    IEnumerator AttackLoop()
    {
        while (isAttacking && currentTarget != null)
        {
            if (!currentTarget.activeInHierarchy)
            {
                isAttacking = false;
                ismove = true;
                currentTarget = null;
                yield break;
            }

            devil devilScript = currentTarget.GetComponent<devil>();
            if (devilScript != null)
            {
                devilScript.hit(power);

                //공격 모션
                Vector3 pushDir = (currentTarget.transform.position - transform.position).normalized;
                pushDir.y = currentTarget.transform.position.y;
                currentTarget.transform.position += pushDir * 0.2f; 
            }

            yield return new WaitForSeconds(1.0f); // 공격 간격
        }
    }

    public void wait_adelie()
    {
        StartCoroutine(wait2());
    }

    IEnumerator wait2()
    {
        yield return new WaitForSeconds(1f);
        adelie_push = false;
    }
}
