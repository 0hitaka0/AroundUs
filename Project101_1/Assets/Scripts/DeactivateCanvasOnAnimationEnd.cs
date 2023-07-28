using UnityEngine;
using UnityEngine.Playables;

public class DeactivateCanvasOnDirectorEnd : MonoBehaviour
{
    public PlayableDirector director;

    void Start()
    {
        // Get the PlayableDirector component attached to the Canvas GameObject
        director = GetComponent<PlayableDirector>();

        // Play the activation timeline
        director.Play();

        // Start a coroutine to handle the deactivation after the timeline finishes playing
        StartCoroutine(DeactivateAfterTimeline());
    }

    private System.Collections.IEnumerator DeactivateAfterTimeline()
    {
        // Wait until the timeline finishes playing
        while (director.state != PlayState.Paused)
        {
            yield return null;
        }

        // Deactivate the Canvas GameObject
        DeactivateActivationCanvas();
    }

    private void DeactivateActivationCanvas()
    {
        // Deactivate the GameObject that contains the Canvas component to visually "deactivate" it
        gameObject.SetActive(false);
    }
}
