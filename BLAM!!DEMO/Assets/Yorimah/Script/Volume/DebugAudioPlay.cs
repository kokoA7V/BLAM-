using UnityEngine;

public class DebugAudioPlay : MonoBehaviour
{
    [SerializeField,Header("SE�T�E���h")]
    AudioSource seAudioSource;

    private void Start()
    {
        Debug.Log("Space�L�[��SE�Đ�");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seAudioSource.Play();
        }
    }
}