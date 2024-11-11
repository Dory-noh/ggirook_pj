using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class devil : MonoBehaviour
{
    public Image hp_img;
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

    int maxhp;
    public int hp;
    Vector3 dir;
    bool ismove;

    // Start is called before the first frame update
    void Start()
    {
        ismove = true;
        maxhp = 10;
        hp = maxhp;
        e_State = EnemyState.Move;
        nest = GameObject.FindGameObjectWithTag("nest");
        nest_hp_manager = GameObject.FindGameObjectWithTag("nest_hp_manager");
        ui_manager = GameObject.FindGameObjectWithTag("ui_manager");
        StartCoroutine(Move());
        StartCoroutine(Attack());
        hp_img = gameObject.transform.GetComponentInChildren<Canvas>().transform.GetChild(1).GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        hp_img.fillAmount = (float)hp / (float)maxhp;
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
            yield return new WaitForSeconds(0.1f);
            if (ismove)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(nest.transform.position.x + 2, (nest.transform.position.y + 0.3f * transform.position.y), 0.35f), 0.01f * 2f);
                //if(Vector3.Distance(gull.transform.position,))
            }
            else
            {

            }

        }
    }
    public void hit()
    {
        StartCoroutine(damage(3));
        if (hp < 0) //Á×À½
        {
            Destroy(gameObject);
            ui_manager.GetComponent<ui_manager>().coin+=2;
        }
    }
    
    IEnumerator damage(int power)
    {
        yield return new WaitForSeconds(0.1f);
        hp -= power;
        transform.position = new Vector3(transform.position.x + 0.3f,transform.position.y,transform.position.z);
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.3f);
        ismove = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("gull"))
        {
            ismove = false;
            other.transform.GetComponent<gull>().hit();
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (Vector3.Distance(transform.position, nest.transform.position) < 3f)
            {
                nest_hp_manager.GetComponent<nest_hp_manager>().hp -= 3;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    
}
