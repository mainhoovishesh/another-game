using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
public class List_Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    public string url;
    public Image Image;
    public RawImage RawImage;
    public Vector2 CenterPosition;
    public Vector2 CenterScale;
    public List<ListBox> ListBoxes;
    [Range(0,100)]
    public int DistanceBetweenListBoxes;

    

    void Start()
    {
        StartCoroutine(DownloadImage(url));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        RawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            
    }
    public void Initial_Position_Scroller()
    {

    }
}
