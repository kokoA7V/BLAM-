using UnityEngine;

public class DebugAudioPlay : MonoBehaviour
{
    [SerializeField,Header("SEサウンド")]
    AudioSource seAudioSource;
    [SerializeField]
    AudioClip se;
    [SerializeField]
    AudioClip bgm;



    private void Start()
    {
        Debug.Log("SpaceキーでSE再生");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seAudioSource.PlayOneShot(se);
            seAudioSource.PlayOneShot(bgm);
            
        }
    }
}