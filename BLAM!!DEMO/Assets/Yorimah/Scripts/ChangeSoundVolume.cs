using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class ChangeSoundVolume : MonoBehaviour
{
    [SerializeField,Header("ミキサー")]
    private AudioMixer audioMixer;

    [SerializeField, Header("MASTERボリュームスライダー")]
    private Slider masterSlider;

    [SerializeField, Header("BGMボリュームスライダー")]
    private Slider bgmSlider;

    [SerializeField, Header("SEボリュームスライダー")]
    private Slider seSlider;

    [SerializeField, Header("MASTERミュートトグル")]
    private Toggle masterToggle;

    [SerializeField, Header("BGMミュートトグル")]
    private Toggle bgmToggle;

    [SerializeField, Header("SEミュートトグル")]
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
    /// セーブ
    /// </summary>
    public void Save()
    {
#if UNITY_EDITOR
        //UnityEditor上なら
        //Assetファイルの中のSaveファイルのパスを入れる
        string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //セーブファイルのパスを設定
        string SaveFilePath = path + "/SoundVolume.bytes";

        // セーブデータの作成
        SaveData saveData = CreateSaveData();

        // セーブデータをJSON形式の文字列に変換
        string jsonString = JsonUtility.ToJson(saveData);

        // 文字列をbyte配列に変換
        byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

        // AES暗号化
        byte[] arrEncrypted = AesEncrypt(bytes);

        // 指定したパスにファイルを作成
        FileStream file = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write);

        //ファイルに保存する
        try
        {
            // ファイルに保存
            file.Write(arrEncrypted, 0, arrEncrypted.Length);
        }
        finally
        {
            // ファイルを閉じる
            if (file != null)
            {
                file.Close();
            }
        }
    }


    /// <summary>
    /// ロード
    /// </summary>
    public void Load()
    {
#if UNITY_EDITOR
        //UnityEditor上なら
        //Assetファイルの中のSaveファイルのパスを入れる
        string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //セーブファイルのパスを設定
        string SaveFilePath = path + "/SoundVolume.bytes";

        //セーブファイルがあるか
        if (File.Exists(SaveFilePath))
        {
            //ファイルモードをオープンにする
            FileStream file = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);
            try
            {
                // ファイル読み込み
                byte[] arrRead = File.ReadAllBytes(SaveFilePath);

                // 復号化
                byte[] arrDecrypt = AesDecrypt(arrRead);

                // byte配列を文字列に変換
                string decryptStr = Encoding.UTF8.GetString(arrDecrypt);

                // JSON形式の文字列をセーブデータのクラスに変換
                SaveData saveData = JsonUtility.FromJson<SaveData>(decryptStr);

                //データの反映
                ReadData(saveData);

            }
            finally
            {
                // ファイルを閉じる
                if (file != null)
                {
                    file.Close();
                }
            }
        }
        else
        {
            //セーブファイルがない場合
            //初期化
            masterSlider.value = 0;
            bgmSlider.value = 0;
            seSlider.value = 0;
            masterToggle.isOn = true;
            bgmToggle.isOn = true;
            seToggle.isOn = true;
        }
    }


    /// <summary>
    /// セーブデータの作成
    /// </summary>
    /// <returns></returns>
    private SaveData CreateSaveData()
    {
        //セーブデータのインスタンス化
        SaveData saveData = new SaveData();

        //ゲームデータの値をセーブデータに代入
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
    /// データの読み込み（反映）
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
    ///  AesManagedマネージャーを取得
    /// </summary>
    /// <returns></returns>
    private AesManaged GetAesManager()
    {
        //任意の半角英数16文字(Read.csと同じやつに)
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
    /// AES暗号化
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesEncrypt(byte[] byteText)
    {
        // AESマネージャーの取得
        AesManaged aes = GetAesManager();
        // 暗号化
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// <summary>
    /// AES復号化
    /// </summary>
    /// <param name="byteText"></param>
    /// <returns></returns>
    public byte[] AesDecrypt(byte[] byteText)
    {
        // AESマネージャー取得
        var aes = GetAesManager();
        // 復号化
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

    /// <summary>
    /// セーブデータ削除
    /// </summary>
    public void Init()
    {
#if UNITY_EDITOR
        //UnityEditor上なら
        //Assetファイルの中のSaveファイルのパスを入れる
        string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

        //ファイル削除
        File.Delete(path + "/SoundVolume.bytes");

        //リロード
        Load();

        Debug.Log("データの初期化が終わりました");
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
