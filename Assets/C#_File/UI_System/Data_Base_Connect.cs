using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Data_Base_Connect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public bool Is_Can_InsertData = false;
    [SerializeField]public bool Is_Can_GetData = false;
    [SerializeField]public GameObject text;
    public TMP_InputField Name;
    private string[] player_data;

    public void InsertData()
    {
        if (Is_Can_InsertData == true)
        {
            StartCoroutine(InsertDatabase());
            print(Name.text);
        }
        
    }

    public void GetData()
    {
        StartCoroutine(GetDatabase());
        text.gameObject.GetComponent<Text>().text = System.Environment.NewLine + "Refreshing...";
        print("Refresh");
    }

    void Start()
    {
        if (Is_Can_GetData == true)
        {
            StartCoroutine(GetDatabase());
        }
    }

    private IEnumerator InsertDatabase()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", Name.text);
        form.AddField("Score", Point.totalpoint);
        form.AddField("Time", Point.totalTime[0] + Point.totalTime[1] + Point.totalTime[2]);
        WWW www = new WWW("https://mywebphotokong.000webhostapp.com/App/Set_Data_Game.php", form);
        yield return www;
        if (www.text == "Pass_Data")
        {
            print("Yes Database");
            Is_Can_GetData = false;
            Is_Can_InsertData = false;
            SceneManager.LoadScene("Main_Menu");
        }
        else
        {
            print(www.text);
            Is_Can_GetData = true;
            Is_Can_InsertData = true;
        }
    }

    private IEnumerator GetDatabase()
    {
        string player_game_data = System.Environment.NewLine;
        int index = 0;
        WWW data = new WWW("https://mywebphotokong.000webhostapp.com/App/Get_Data_Game.php");
        yield return data;
        player_data = data.text.Split(';');
        if (player_data != null)
        {
            print("num " + player_data.Length);
            foreach (string _text in player_data)
            {
                print(_text);
                player_game_data = player_game_data + _text + System.Environment.NewLine;
            }
            text.gameObject.GetComponent<Text>().text = player_game_data;
        }
    }
}
        
