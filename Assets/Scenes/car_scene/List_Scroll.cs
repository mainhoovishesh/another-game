using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
public class List_Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] url;
    public List<Sprite> ListBoxImage;
    public Vector2 CenterPosition;
    public Vector2 CenterScale;
    public List<ListBox> ListBoxes;
    
    [Range(0,100)]
    public int DistanceBetweenListBoxes;
    public int Total_ListBox;
    private string stringofObject;
    public GameObject[] Weapon;
    public List<string> Texts;
    public GameObject maintext;
    public GameObject Instance;
    
    void Awake()
    {
        //maintext = gameObject.GetComponent<Text>();
        url = new string[Total_ListBox];
        Weapon = new GameObject[Total_ListBox];
        Weapon = GameObject.FindGameObjectsWithTag("Weapons");
        AssigningText();
        CreatingListBox();
    }
    void Start()
    {
        
    }
    public void GettingImage(string url, int i)
    {
        StartCoroutine(DownloadImage(url,i));
    }

    IEnumerator DownloadImage(string MediaUrl,int i)
    {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            Debug.Log(request);
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.Log("Network Error : " +request.error);
            }
            else if(request.isHttpError)
            {
                Debug.Log("HttpError : " +request.error);

            }
            else
            {
                Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                
                ListBoxImage[i] = sprite;
                yield return ListBoxImage[i];
            }
        
    }
    public void Initial_Position_Scroller()
    {
        
    }
    public void CreatingListBox()
    {
        for (int i = 0; i < Total_ListBox; i++)
        {
            GettingImage("https://cfcdnpull-creativefreedoml.netdna-ssl.com/wp-content/uploads/2016/06/New-instagram-icon.jpg",i);
            ListBox listBox = new ListBox(ListBoxImage[i], Texts[i], Weapon[i], InstantitatingPositionListBox());
            ListBoxes.Add(listBox);
            Instantiate(Instance,listBox.Position,Quaternion.identity);
        }
        
    }

    public Vector3 InstantitatingPositionListBox()
    {
        Vector3 _Position;
        if(Total_ListBox%2==1)
        {
            ListBoxes[Total_ListBox+1/2].Position = ListBoxes[Total_ListBox+1/2].gameObject.GetComponent<RectTransform>().position+new Vector3(0,2f,0);
            _Position = ListBoxes[Total_ListBox+1/2].Position;
        }
        else
        {
             ListBoxes[Total_ListBox/2].Position = ListBoxes[Total_ListBox/2].gameObject.GetComponent<RectTransform>().position+new Vector3(0,2f,0);
            _Position = ListBoxes[Total_ListBox/2].Position;
        }
        return _Position;
    }
    public void AssigningText()
    {
        for(int i=0;i<Total_ListBox;i++)
        {
            Text _mainText = maintext.GetComponent<Text>();
            _mainText.text = "instance"+i.ToString();
            Debug.Log(_mainText.text);
            Texts.Add(_mainText.text);
        }
    }
}
