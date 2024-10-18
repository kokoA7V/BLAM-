using UnityEngine;

public class DebugAudioPlay : MonoBehaviour
{
    [SerializeField,Header("SE�T�E���h")]
    AudioSource seAudioSource;
    [SerializeField]
    AudioClip se;
    [SerializeField]
    AudioClip bgm;



    private void Start()
    {
        Debug.Log("Space�L�[��SE�Đ�");
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