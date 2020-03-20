using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Linq;


public class ReceiveResult : MonoBehaviour
{
    public GameObject[] component;
    public GameObject[] textbox;
    public GameObject[] mark;
    public GameObject[] allmark;
    public AudioSource sound;
    public bool[] appearance;   //if the component appears in the text;

    public string[] keywordsWhere = { "where", "position" };
    public string[] keywordsReset = { "reset" };

    public bool where;    // if where word appears
    public bool reset;          // reset scren

    public float k = 250;

    public int choosen = -1;  // The choosen component
    public bool componentFound = false;

    public bool flag = true;
    // Initialization
    void Start()
    {
        component = GameObject.FindGameObjectsWithTag("components");
        allmark = GameObject.FindGameObjectsWithTag("mark");
        appearance = new bool[component.Length];
        int i;
        textbox = new GameObject[component.Length];
        mark = new GameObject[component.Length];

        for (i = 0; i < component.Length; i++)
        {
            for (int j = 0; j < allmark.Length; j++)
            {
                if (allmark[j].name == component[i].name)
                {
                    mark[i] = allmark[j];
                    break;
                }
            }
        }
        sound.volume = 0;
    }
    void searchKeywords(string text)
    {
        int i;
        where = false;
        for (i = 0; i < keywordsWhere.Length; i++)
        {
            if (text.Contains(keywordsWhere[i]))
            {
                where = true;
                break;
            }
        }

        reset = false;
        for (i = 0; i < keywordsReset.Length; i++)
        {
            if (text.Contains(keywordsReset[i]))
            {
                reset = true;
                break;
            }
        }
    }
    void searchComponents(string text)
    {
        int i;
        componentFound = false;
        for (i = 0; i < component.Length; i++)
        {
            if (text.Contains(component[i].name))      //search for the first component appears.
            {
                choosen = i;
                componentFound = true;
                break;
            }
        }
    }
    void resetComponents()
    {
        int i;
        for (i = 0; i < component.Length; i++)
        {
            component[i].SetActive(true);
        }
    }
    void showPosition(int choosen)
    {
        int i;
        for (i = 0; i < component.Length; i++)
        {
            component[i].SetActive(false);
        }
        component[choosen].SetActive(true);
    }

    // Speech text received
    void onActivityResult(string recognizedText)
    {
        char[] delimiterChars = { '~' };
        string[] result = recognizedText.Split(delimiterChars);
        //You can get the number of results with result.Length
        //And access a particular result with result[i] where i is an int
        //I have just assigned the best result to UI text
        result[0] = result[0].ToLower();
        //result[1] = result[1].ToLower();
       // result[2] = result[3].ToLower();
        GameObject.Find("Text").GetComponent<Text>().text = "";
        choosen = -1;
        componentFound = false;
        searchKeywords(result[0]);
        //searchKeywords(result[1]);
        //searchKeywords(result[2]);

        searchComponents(result[0]);
        //searchComponents(result[1]);
        //searchComponents(result[2]);

        if (reset) resetComponents();

        if (where && componentFound)
        {
            showPosition(choosen);
        }



    }

    void Update()
    {
        if (choosen > -1)
        {
            //sound.volume = 0;
            //GameObject.Find("Text").GetComponent<Text>().text = "asdasd";
            GameObject board;
            board = GameObject.Find("MotherBoard").gameObject;
            float x3 = component[choosen].transform.position.x;
            float y3 = component[choosen].transform.position.y;
            float z3 = component[choosen].transform.position.z;
            float x1 = board.transform.position.x;
            float y1 = board.transform.position.y;
            float z1 = board.transform.position.z;
            float x2 = mark[choosen].transform.position.x;
            float y2 = mark[choosen].transform.position.y;
            float z2 = mark[choosen].transform.position.z;
            //float x3 = x2-x1;
            //float y3 = y2-y1;
            //float z3 = z2-z1;
            //float x4=getPose

            //sound.volume = (float)1 - (x * x + y * y) / k;
            /**
            string s;
            s = x1.ToString("F2") + "\t " + y1.ToString("F2") + "\t " + z1.ToString("F2") + "\n" + x2.ToString("F2") + "\t " + y2.ToString("F2") + "\t " + z2.ToString("F2") +
                "\n" + x3.ToString("F2") + "\t " + y3.ToString("F2") + "\t " + z3.ToString("F2")
                + "\t " + (x3 * x3 + y3 * y3 + z3 * z3).ToString("F3");
            GameObject.Find("Text").GetComponent<Text>().text = s;
            **/
            if ((Mathf.Abs(x1-x2)<=0.03)&&(Mathf.Abs(z1-z3)<=0.1))
            {
                sound.volume = 1;   
            }
            else
            {
                sound.volume = 0;
                //flag = true;
            }

        }
        else sound.volume = 0;
    }
}