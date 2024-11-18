using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class devil : MonoBehaviour
{
    public Image hp_img;
    public bool adelie_push;
    enum EnemyState
    {
        Move,
        Attack,
        Damaged,
        Die
    }
    EnemyState e_State;
    public GameObject nest;
    public GameObject nest_hp_manager;
    public CharacterController devil_cc;
    public GameObject ui_manager;
    int power;
    int maxhp;
    public int hp;
    Vector3 dir;
    bool ismove;
    bool change_img;
    int point;
    float speed;
    //public bool time_changer;
    float origin_y;
    Vector3 velo = Vector3.zero;
    Vector3 targetPos;
    public Text damage_text;
    // Start is called before the first frame update
    void Start()
    {
        adelie_push = false;
        ismove = true;
        origin_y = transform.position.y;
        if(transform.CompareTag("enemy_gull"))
        {
            maxhp = 50;
            power = 5;
            point = 35;
            speed = 0.2f;
        }
        else if (transform.CompareTag("enemy_fox"))
        {
            maxhp = 800;
            power = 30;
            point = 80;
            speed = 0.4f;
        }
        else if (transform.CompareTag("enemy_pelican"))
        {
            maxhp = 2000;
            power = 60;
            point = 150;
            speed = 0.25f;
        }
        hp = maxhp;
        e_State = EnemyState.Move;
        nest = GameObject.FindGameObjectWithTag("nest");
        nest_hp_manager = GameObject.FindGameObjectWithTag("nest_hp_manager");
        ui_manager = GameObject.FindGameObjectWithTag("ui_manager");
        change_img = true;
        transform.GetChild(0).gameObject.SetActive(change_img);
        transform.GetChild(1).gameObject.SetActive(!change_img);
        StartCoroutine(Move());
        targetPos = new Vector3(nest.transform.position.x + 2, (nest.transform.position.y + 0.3f * origin_y), 0.35f);
        hp_img = gameObject.transform.GetComponentInChildren<Canvas>().transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hp_img.fillAmount = (float)hp / (float)maxhp;
        if (transform.CompareTag("enemy_gull"))
        {
            damage_text.text = hp.ToString();
        }
        //switch (e_State)
        //{
        //    case EnemyState.Move:
        //        Move();
        //        break;
        //    case EnemyState.Attack:
        //        Attack();
        //        break;
        //    case EnemyState.Damaged:
        //        //Damaged();
        //        break;
        //    case EnemyState.Die:
        //        //Die();
        //        break;
        //}
    }
    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.06f);
            if (ismove)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velo, speed*10f);
                change_img = !change_img;
                transform.GetChild(0).gameObject.SetActive(change_img);
                transform.GetChild(1).gameObject.SetActive(!change_img);
                
                //if(Vector3.Distance(gull.transform.position,))
            }
            else
            {

            }

        }
    }
    public void hit(int power)
    {
        StartCoroutine(damage(power)); //devil의 데미지를 깎는 함수
        if (hp < 0) //죽음
        {
            Destroy(gameObject);
            ui_manager.GetComponent<ui_manager>().coin+=point;
            ui_manager.GetComponent<ui_manager>().count_text.text = (++ui_manager.GetComponent<ui_manager>().count).ToString();

        }
    }
    
    IEnumerator damage(int power)
    {
        yield return new WaitForSeconds(0.1f);
        hp -= power;
        transform.position = new Vector3(transform.position.x + 0.3f,origin_y,transform.position.z);
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(transform.position.x - 0.2f, origin_y, transform.position.z);
        yield return new WaitForSeconds(0.3f);
        ismove = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("nest"))
        {

            nest_hp_manager.GetComponent<nest_hp_manager>().hp -= power;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            StartCoroutine(wait());
        }
        if (other.gameObject.tag.StartsWith("gull"))
        {
            ismove = false;
            other.transform.GetComponent<gull>().hit(power);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator wait2()
    {
        yield return new WaitForSeconds(1f);
        adelie_push = false;
    }
    public void wait_adelie()
    {
        StartCoroutine(wait2());
        
    }
    
}
