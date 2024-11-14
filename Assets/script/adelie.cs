using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adelie : MonoBehaviour
{
    float speed = 0.1f;
    public Vector3 pos;
    public int dir;
    GameObject adelie_manager;
    public GameObject nest_hp_manager;
    public bool time_changer;
    // Start is called before the first frame update
    void Start()
    {
        adelie_manager = GameObject.FindGameObjectWithTag("adelie_manager");
        dir = adelie_manager.GetComponent<adelie_manager>().dir == 0 ? 1 : 0; // ��ȯ�� dir = 1 -> ���������� �̵�, dir = 0 -> �������� �̵�
        pos = adelie_manager.GetComponent<adelie_manager>().pos[dir];
        nest_hp_manager = GameObject.FindGameObjectWithTag("nest_hp_manager");
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(speed);
            transform.position = Vector3.Lerp(transform.position, pos, 0.01f * 2f);
            if (Vector3.Distance(transform.position, pos) < 3) Destroy(gameObject);
            //change_img = !change_img;
            //transform.GetChild(0).gameObject.SetActive(change_img);
            //transform.GetChild(1).gameObject.SetActive(!change_img);
            //if(Vector3.Distance(gull.transform.position,))


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("nest") )
        {
            time_changer = nest_hp_manager.GetComponent<nest_hp_manager>().time_changer;
            if (!time_changer)
            {
                nest_hp_manager.GetComponent<nest_hp_manager>().hp -= 1;
                nest_hp_manager.GetComponent<nest_hp_manager>().wait_adelie();
            }
            
        }
        if (other.gameObject.tag.StartsWith("gull"))
        {
            time_changer = other.transform.GetComponent<gull>().time_changer;
        }
        else if (other.gameObject.tag.StartsWith("enemy"))
        {
            time_changer = other.transform.GetComponent<devil>().time_changer;
        }
        if (!time_changer)
        {
            // y���� ���������� ũ�ų� ������ y�� ������ ���� �ʴ´�. ��Ÿ�� �� �ٽ� ��ĥ �� �ֵ��� �Ѵ�.
            float x = 1;
            float y = 1;
            if (dir == 0) x *= -1;
            if (other.gameObject.transform.position.y < gameObject.transform.position.y)
            {
                y *= -1;
                if (other.gameObject.transform.position.y < -0.65f) y = 0;
            }
            else
            {
                if (other.gameObject.transform.position.y > 1.5f) y = 0;
            }
            if (other.gameObject.tag.StartsWith("gull") && other.GetComponent<gull>().adelie_push == false)
            {
                other.GetComponent<gull>().adelie_push = true;
                other.gameObject.transform.position += new Vector3(x * 1f, y * 1f, 0) * 2;
                other.GetComponent<gull>().wait_adelie();
                
            }
            else if (other.gameObject.tag.StartsWith("enemy") && other.GetComponent<devil>().adelie_push == false)
            {
                other.GetComponent<devil>().adelie_push = true;
                if (other.gameObject.transform.position.x < -0.8f) x = 0;
                other.gameObject.transform.position += new Vector3(x * 1f, y * 1f, 0) * 2;
                other.GetComponent<devil>().wait_adelie();
            }
        }
        

    }
}
