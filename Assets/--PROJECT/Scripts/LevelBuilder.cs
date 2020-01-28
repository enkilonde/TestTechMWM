using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("References")]
    public AnalyseLevelMakerFile analyser;
    public LineRenderer middleLine;
    public GameObject switcherPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        analyser.OnEndAnalyse += OnEndAnalyse;
    }
    private void OnDestroy()
    {
        analyser.OnEndAnalyse -= OnEndAnalyse;
    }


    private void OnEndAnalyse()
    {

        middleLine.SetPosition(1, new Vector3(0, analyser.levelMakerFile.duration, 0));
        List<GPGameEvent> events = analyser.levelMakerFile.events;
        for (int i = 0; i < events.Count; i++)
        {
            float timing = (events[i].startTime + events[i].endTime) / 2;

            GameObject switcher = Instantiate(switcherPrefab, new Vector3(0, timing, 0), Quaternion.identity, transform);

            float duration = events[i].endTime - events[i].startTime;

            switcher.transform.localScale = Vector3.one * duration;
        }
    }

}
