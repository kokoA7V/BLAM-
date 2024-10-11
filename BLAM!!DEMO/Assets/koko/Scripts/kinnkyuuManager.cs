using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kinnkyuuManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    Player player;

    [SerializeField]
    AttackPattern attackPattern;

    [SerializeField]
    GameObject nowObj;

    bool counterSE = false;
    //bool enemyShotSE = false;

    private void Start()
    {
        Time.timeScale = 1f;

        BgmManager.Instance.Play("Stage1BGM");
    }

    private void Update()
    {
        nowObj = enemy.transform.GetChild(5).gameObject;
        attackPattern = nowObj.GetComponent<AttackPattern>();

        if (attackPattern.CanCounter)
        {
            Time.timeScale = 0.3f;
            Debug.Log("Slow!!!");
        }
        else
        {
            Time.timeScale = 1;
        }

        // カウンターSE
        if (attackPattern.CanCounter)
        {
            if (!counterSE)
            {
                SeManager.Instance.Play("Counter");
                counterSE = true;
            }
        }
        else
        {
            counterSE = false;
        }

        // レイルガンSE めんどうやめ
        //if (enemy.GetComponent<Enemy>().animDic["Shot"])
        //{
        //    if (!enemyShotSE)
        //    {
        //        SeManager.Instance.Play("RailGun");
        //        enemyShotSE = true;
        //    }
        //}
        //else
        //{
        //    enemyShotSE = false;
        //}


        // エネミーHP管理
        if (enemy.GetComponent<Enemy>().Hp < 0)
        {
            Debug.Log("倒した");
            SceneManager.LoadScene("TitleScene");
        }

        // プレイヤーHP管理
        if (player.Hp < 0)
        {
            Debug.Log("死亡");
            SceneManager.LoadScene("TitleScene");
        }
    }
}
