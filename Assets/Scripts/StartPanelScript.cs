using UnityEngine;

public class StartPanelScript : MonoBehaviour
{
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    [SerializeField] private AudioClip soundClick;

    public void OnSelect()
    {
        source.PlayOneShot(soundClick);
    }
}
