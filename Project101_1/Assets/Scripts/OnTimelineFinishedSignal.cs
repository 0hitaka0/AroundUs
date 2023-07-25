using UnityEngine;
using UnityEngine.SceneManagement;
public class TimelineSignalReceiver : MonoBehaviour
{
    public void OnTimelineFinishedSignal()
    {
        SceneManager.LoadScene("CutsceneHouse");
    }
}
