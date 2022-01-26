using System.Numerics;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using System.Text;

public class Realtimedatabase : MonoBehaviour
{
    DatabaseReference reference;
    [SerializeField]
    InputField Username;
    [SerializeField]
    InputField ValuesEntered;
    [SerializeField]
    Text Data;
    [SerializeField]
    InputField Email;

    public Transform Player_WithPosition;
    public UnityEngine.Vector3 PlayerCurrentPosition, Player_PrevPosition;
    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
                PlayerCurrentPosition = Player_WithPosition.localPosition;
        SendingData();
        //Debug.Log(FirebaseApp.getInstance().getOptions().getDatabaseUrl());
    }
    public void savedata()
    {
        User user = new User();
        user.Email = Email.text;
        user.UserName = Username.text;
        string Json = JsonUtility.ToJson(user);
        reference.Child("User").Child(user.UserName).SetRawJsonValueAsync(Json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Successfuly added data to firebase");
            }
            else
            {
                Debug.Log("Not COmpleted");
            }
        });
    }
    public void SendingData()
    {
        Player_PrevPosition = PlayerCurrentPosition;

        User user = new User();
        user.currentposition = Player_PrevPosition;
        string Json = JsonUtility.ToJson(user);
        reference.Child("User").Child(SerializeVector3Array(user.currentposition)).SetRawJsonValueAsync(Json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Position Updated successfully");


            }
            else
            {
                Debug.Log("Not COmpleted");
            }
        });
    }
    public static string SerializeVector3Array(UnityEngine.Vector3 v)
    {
        StringBuilder sb = new StringBuilder();
        
            sb.Append(v.x).Append(" ").Append(v.y).Append(" ").Append(v.z).Append("|");
        
        if (sb.Length > 0) // remove last "|"
            sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
    public static UnityEngine.Vector3 DeserializeVector3Array(string aData)
    {
        string[] vectors = aData.Split('|');
        UnityEngine.Vector3 result = new UnityEngine.Vector3();
        for (int i = 0; i < vectors.Length; i++)
        {
            string[] values = vectors[i].Split(' ');
            if (values.Length != 3)
                throw new System.FormatException("component count mismatch. Expected 3 components but got " + values.Length);
            
            result = new UnityEngine.Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
        return result;
    }
    public void loaddata()
    {
        reference.Child("User").Child(ValuesEntered.text).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Child("UserName").Value.ToString());
                Debug.Log(snapshot.Child("Email").Value.ToString());
            }
            else
            {
                Debug.Log("Not COmpleted");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCurrentPosition = Player_WithPosition.localPosition;
        if (Player_PrevPosition != PlayerCurrentPosition)
        {
            SendingData();
        }
    }
}
