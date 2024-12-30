using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class login_manager : MonoBehaviour
{
    public InputField id;
    public InputField password;
    public Login_Data loginData;
    public Text notify;
    TextAsset login_text;
    public Text test_txt;
    //Application.dataPath + "/text/login.txt";
    string login_text_str;
    List<string> login_list;
    public GameObject login_panel;
    //public GameObject signup_panel;
    string filePath;
    string text;
    public Button bt1;
    public Button bt2;
    bool issignup;

    // Start is called before the first frame update
    void Start()
    {
        issignup = false;
        CopyFileFromResources();
        login_panel = GameObject.FindGameObjectWithTag("login_panel");
        notify = GameObject.FindGameObjectWithTag("notify").GetComponent<Text>();
        id = GameObject.FindGameObjectWithTag("login_id_input").GetComponent<InputField>();
        password = GameObject.FindGameObjectWithTag("login_password_input").GetComponent<InputField>();
        bt1 = GameObject.FindGameObjectWithTag("login_bt1").GetComponent<Button>();
        bt2 = GameObject.FindGameObjectWithTag("login_bt2").GetComponent<Button>();
        //if (PlayerPrefs.HasKey("Username"))
        //{
        //    loginData.Username = PlayerPrefs.GetString("Username");
        //    loginData.Token = PlayerPrefs.GetString("Token");
        //    Debug.Log("�α��� ���� ���� �Ϸ�!");
        //}
        //else
        //{
        //    Debug.Log("����� �α��� ������ �����ϴ�.");
        //}
        Debug.Log(loginData.Username);
        if (loginData.Username == "")
        {
            login_panel.SetActive(true);
        }
        else
        { 
            login_panel.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //refresh_info();
        //test_txt.text = login_text_str;
    }
    void CopyFileFromResources()
    {
        // Resources���� ���� �ε�
        TextAsset textAsset = login_text;
        filePath = Path.Combine(Application.persistentDataPath, "login.txt");

        // ������ ������ ����
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, textAsset.text);
            Debug.Log("���� ���� ����! ���: " + filePath);
        }
        else
        {
            Debug.Log("������ �̹� �����մϴ�!");
        }
    }

    void refresh_info()
    {
        string fileContent = File.ReadAllText(filePath);
        login_text_str = fileContent;
        test_txt.text = login_text_str;
        login_list = login_text_str.Split(' ').ToList();
    }

    public void login()
    {
        refresh_info();
        for (int i = 0; i < login_list.Count; i++)
        {
            string temp_id = login_list[i].Split(',')[0];
            string temp_password = login_list[i].Split(',')[1];
            if(id.text == temp_id)
            {
                if(password.text == temp_password)
                {
                    login_panel.SetActive(false);
                    loginData.Username = id.text;
                    loginData.Token = password.text;
                    //PlayerPrefs.SetString("Username", loginData.Username);
                    //PlayerPrefs.SetString("Token", loginData.Token);
                    //PlayerPrefs.Save();
                    break;
                }
                else
                {
                    notify.text = "��й�ȣ�� Ʋ�Ƚ��ϴ�. �ٽ� �Է����ּ���.";
                    break;
                }
            }
            else
            {
                notify.text = "��ϵ��� ���� ������Դϴ�. �ٽ� �Է����ּ���.";
            }
        }
       
    }

    public void open_signup()
    {
        if (issignup)
        {
            bt1.transform.GetChild(0).GetComponent<Text>().text = "ȸ�� ����";
            bt2.transform.GetChild(0).GetComponent<Text>().text = "�α���";
            issignup = false;
            bt1.onClick.AddListener(open_signup);
            bt2.onClick.AddListener(login);
        }
        else
        {
            bt1.transform.GetChild(0).GetComponent<Text>().text = "���ư���";
            bt2.transform.GetChild(0).GetComponent<Text>().text = "�����ϱ�";
            bt1.onClick.AddListener(open_login);
            bt2.onClick.AddListener(do_signup);
        }
    }
    void open_login()
    {
        issignup = true;
        open_signup();
    }

    public void do_signup()
    {
        refresh_info();
        if(id.text == "" || id.text == " "|| password.text == "" || password.text == " ") notify.text = "������ id, password�� ����� �� �����ϴ�.";
        else
        {
            for (int i = 0; i < login_list.Count; i++)
            {
                string temp_id = login_list[i].Split(',')[0];
                if (id.text == temp_id)
                {
                    notify.text = "��� ���� ���̵��Դϴ�. �ٸ� ���̵� �Է����ּ���.";
                    break;
                }
                else
                {
                    if (i == login_list.Count - 1)
                    {
                        login_text_str += (' '+id.text +','+ password.text);
                        File.WriteAllText(filePath, login_text_str);
                        notify.text = "��ϵǾ����ϴ�. �α������ּ���.";
                        id.text = "";
                        password.text = "";
                        issignup = true;
                        open_signup();
                    }
                }
            }
        }
        
        
    }
}
