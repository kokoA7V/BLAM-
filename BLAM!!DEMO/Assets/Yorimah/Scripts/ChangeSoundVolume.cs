using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class ChangeSoundVolume : MonoBehaviour
{
    [SerializeField,Header("�~�L�T�[")]
    private AudioMixer audioMixer;

    [SerializeField, Header("MASTER�{�����[���X���C�_�[")]
    private Slider masterSlider;

    [SerializeField, Header("BGM�{�����[���X���C�_�[")]
    private Slider bgmSlider;

    [SerializeField, Header("SE�{�����[���X���C�_�[")]
    private Slider seSlider;

    [SerializeField, Header("MASTER�~���[�g�g�O��")]
    private Toggle masterToggle;

    [SerializeField, Header("BGM�~���[�g�g�O��")]
    private Toggle bgmToggle;

    [SerializeField, Header("SE�~���[�g�g�O��")]
    private Toggle seToggle;

    private void Start()
    {
        Load();
    }

    private void OnDestroy()
    {
        Save();
    }

    public void SetBGM(float volume)
    {
        if (volume <= -50f)
        {
            volume = -80f;
            bgmToggle.isOn = false;
        }
        audioMixer.SetFloat("BgmVolume", volume);
    }

    public void SetSE(float volume)
    {
        if (volume <= -50f)
        {
            volume = -80f;
            seToggle.isOn = false;
        }
        audioMixer.SetFloat("SeVolume", volume);
    }

    public void SetMASTER(float volume)
    {
        if (volume <= -50f)
        {
            volume = -80f;
            masterToggle.isOn = false;
        }
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void MuteMASTER(bool mute)
    {
        float vol;
        if (!mute) vol = -80f;
        else vol = masterSlider.value;

        audioMixer.SetFloat("MasterVolume", vol);
    }

    public void MuteBGM(bool mute)
    {
        float vol;
        if (!mute) vol = -80f;
        else vol = bgmSlider.value;

        audioMixer.SetFloat("BgmVolume", vol);
    }

    public void MuteSE(bool mute)
    {
        float vol;
        if (!mute) vol = -80f;
        else vol = seSlider.value;

        audioMixer.SetFloat("SeVolume", vol);
    }


    /// <summary>
    /// �Z�[�u
    /// </summary>
    public void Save()
    {
#if UNITY_EDITOR
        //UnityEditor��Ȃ�
        //Asset�t�@�C���̒���Save�t�@�C���̃p�X������
        string path = Application.dataPath + "/Save";

#else
        //�����łȂ����
        //.exe������Ƃ����Save�t�@�C�����쐬�������̃p�X������
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //�Z�[�u�t�@�C���̃p�X��ݒ�
        string SaveFilePath = path + "/SoundVolume.bytes";

        // �Z�[�u�f�[�^�̍쐬
        SaveData saveData = CreateSaveData();

        // �Z�[�u�f�[�^��JSON�`���̕�����ɕϊ�
        string jsonString = JsonUtility.ToJson(saveData);

        // �������byte�z��ɕϊ�
        byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

        // AES�Í���
        byte[] arrEncrypted = AesEncrypt(bytes);

        // �w�肵���p�X�Ƀt�@�C�����쐬
        FileStream file = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write);

        //�t�@�C���ɕۑ�����
        try
        {
            // �t�@�C���ɕۑ�
            file.Write(arrEncrypted, 0, arrEncrypted.Length);
        }
        finally
        {
            // �t�@�C�������
            if (file != null)
            {
                file.Close();
            }
        }
    }


    /// <summary>
    /// ���[�h
    /// </summary>
    public void Load()
    {
#if UNITY_EDITOR
        //UnityEditor��Ȃ�
        //Asset�t�@�C���̒���Save�t�@�C���̃p�X������
        string path = Application.dataPath + "/Save";

#else
        //�����łȂ����
        //.exe������Ƃ����Save�t�@�C�����쐬�������̃p�X������
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //�Z�[�u�t�@�C���̃p�X��ݒ�
        string SaveFilePath = path + "/SoundVolume.bytes";

        //�Z�[�u�t�@�C�������邩
        if (File.Exists(SaveFilePath))
        {
            //�t�@�C�����[�h���I�[�v���ɂ���
            FileStream file = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);
            try
            {
                // �t�@�C���ǂݍ���
                byte[] arrRead = File.ReadAllBytes(SaveFilePath);

                // ������
                byte[] arrDecrypt = AesDecrypt(arrRead);

                // byte�z��𕶎���ɕϊ�
                string decryptStr = Encoding.UTF8.GetString(arrDecrypt);

                // JSON�`���̕�������Z�[�u�f�[�^�̃N���X�ɕϊ�
                SaveData saveData = JsonUtility.FromJson<SaveData>(decryptStr);

                //�f�[�^�̔��f
                ReadData(saveData);

            }
            finally
            {
                // �t�@�C�������
                if (file != null)
                {
                    file.Close();
                }
            }
        }
        else
        {
            //�Z�[�u�t�@�C�����Ȃ��ꍇ
            //������
            masterSlider.value = 0;
            bgmSlider.value = 0;
            seSlider.value = 0;
            masterToggle.isOn = true;
            bgmToggle.isOn = true;
            seToggle.isOn = true;
        }
    }


    /// <summary>
    /// �Z�[�u�f�[�^�̍쐬
    /// </summary>
    /// <returns></returns>
    private SaveData CreateSaveData()
    {
        //�Z�[�u�f�[�^�̃C���X�^���X��
        SaveData saveData = new SaveData();

        //�Q�[���f�[�^�̒l���Z�[�u�f�[�^�ɑ��
        //Master
        saveData.masVol = masterSlider.value;   
        saveData.masFlg = masterToggle.isOn;

        //Bgm
        saveData.bgmVol = bgmSlider.value;
        saveData.bgmFlg = bgmToggle.isOn;

        //Se
        saveData.seVol = seSlider.value;
        saveData.seFlg = seToggle.isOn;

        return saveData;
    }

    /// <summary>
    /// �f�[�^�̓ǂݍ��݁i���f�j
    /// </summary>
    /// <param name="saveData"></param>
    private void ReadData(SaveData saveData)
    {
        float vol;

        //Master
        if (!saveData.masFlg)
        {
            masterToggle.isOn = false;
            vol = -80f;
        }
        else
        {
            masterToggle.isOn = true;
            vol = saveData.masVol;
        }
        masterSlider.value = saveData.masVol;
        audioMixer.SetFloat("MasterVolume", vol);

        //Bgm
        if (!saveData.bgmFlg)
        {
            bgmToggle.isOn = false;
            vol = -80f;
        }
        else
        {
            bgmToggle.isOn = true;
            vol = saveData.bgmVol;
        }
        bgmSlider.value = saveData.bgmVol;
        audioMixer.SetFloat("BgmVolume", vol);

        //Se
        if (!saveData.seFlg)
        {
            seToggle.isOn = false;
            vol = -80f;
        }
        else
        {
            seToggle.isOn = true;
            vol = saveData.seVol;
        }
        seSlider.value = saveData.seVol;
        audioMixer.SetFloat("SeVolume", vol);
    }



    /// <summary>
    ///  AesManaged�}�l�[�W���[���擾
    /// </summary>
    /// <returns></returns>
    private AesManaged GetAesManager()
    {
        //�C�ӂ̔��p�p��16����(Read.cs�Ɠ������)
        string aesIv = "5467979454867416";
        string aesKey = "7875321532474526";

        AesManaged aes = new AesManaged();
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.IV = Encoding.UTF8.GetBytes(aesIv);
        aes.Key = Encoding.UTF8.GetBytes(aesKey);
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }

    /// <summary>
    /// AES�Í���
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesEncrypt(byte[] byteText)
    {
        // AES�}�l�[�W���[�̎擾
        AesManaged aes = GetAesManager();
        // �Í���
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// <summary>
    /// AES������
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesDecrypt(byte[] byteText)
    {
        // AES�}�l�[�W���[�擾
        var aes = GetAesManager();
        // ������
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

    /// <summary>
    /// �Z�[�u�f�[�^�폜
    /// </summary>
    public void Init()
    {
#if UNITY_EDITOR
        //UnityEditor��Ȃ�
        //Asset�t�@�C���̒���Save�t�@�C���̃p�X������
        string path = Application.dataPath + "/Save";

#else
        //�����łȂ����
        //.exe������Ƃ����Save�t�@�C�����쐬�������̃p�X������
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //�t�@�C���폜
        File.Delete(path + "/SoundVolume.bytes");

        //�����[�h
        Load();

        Debug.Log("�f�[�^�̏��������I���܂���");
    }
}

[System.Serializable]
public class SaveData
{
    public float masVol;
    public float bgmVol;
    public float seVol;
    public bool masFlg;
    public bool bgmFlg;
    public bool seFlg;
}
