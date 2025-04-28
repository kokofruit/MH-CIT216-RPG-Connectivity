using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetAvatars : MonoBehaviour
{
    [SerializeField] Button getButton;

    // Start is called before the first frame update
    void Start()
    {
        getButton.onClick.AddListener(() => StartCoroutine("GetResults"));
    }

    IEnumerator GetResults()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://192.168.56.101/list.php"))
        {
            //PlayerSettings.InsecureHttpOption.AlwaysAllowed;
            
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

}
