using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AnalyseLevelMakerFile : MonoBehaviour
{

    public string musicName;

    public GPGameLevelMakerFile levelMakerFile;

    public System.Action OnEndAnalyse;

    [HideInInspector]
    public AudioClip clip;

    IEnumerator Start()
    {
        yield return null; //J'attend une frame, le temps de donner à tout les autres scripts de s'accrocher à l'event OnEndAnalyse.
        Read();
        GetClip();
    }

    /// <summary>
    /// pour que ça marche en build et que on ai juste à imput un nom de musique, et il trouve le path auto
    /// </summary>
    /// <returns></returns>
    public string GetPath()
    {
        string path = Application.dataPath + "/Resources/" + musicName + "/" + musicName + "_jeu.glm.bytes";//temp
        //Debug.Log(path);
        return path;
    }

    public void GetClip()
    {
        clip = Resources.Load<AudioClip>(musicName + "/" + musicName + "_sound");
    }

    public void Read()
    {
        StreamReader reader = new StreamReader(GetPath());

        levelMakerFile = JsonUtility.FromJson<GPGameLevelMakerFile>(reader.ReadToEnd());

        if(OnEndAnalyse != null)
            OnEndAnalyse.Invoke();
    }

}
