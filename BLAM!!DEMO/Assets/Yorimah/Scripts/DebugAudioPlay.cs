using UnityEngine;

public class DebugAudioPlay : MonoBehaviour
{
    [SerializeField,Header("SEサウンド")]
    AudioSource seAudioSource;

    private void Start()
    {
        Debug.Log("SpaceキーでSE再生");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seAudioSource.Play();
        }
    }
}