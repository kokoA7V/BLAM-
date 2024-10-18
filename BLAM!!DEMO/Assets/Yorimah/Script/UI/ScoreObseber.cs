using UnityEngine;
using UnityEngine.UI;

public class ScoreObseber : MonoBehaviour
{
    [SerializeField, Header("えねみ～")]
    private GameObject enemyGameObject;

    private Enemy enemyScript;

    private GameObject attackPatternObject;

    [SerializeField, Header("ぷれいや～")]
    private Player playerScript;

    private int playerCombo;
    private int playerComboScore;

    private int maxCombo;

    private int deffenceScore;

    private float stagePlayTime;

    private AttackPattern attackPattern;

    [SerializeField, Header("攻撃評価のテキスト")]
    private Text attackScoreText;

    [SerializeField, Header("時間評価のテキスト")]
    private Text timeScoreText;

    [SerializeField, Header("防御評価のテキスト")]
    private Text defennsedScoreText;

    [SerializeField, Header("総合評価のテキスト")]
    private Text summryScoreText;


    [SerializeField, Header("リザルトのまとめテキスト")]
    private GameObject resultGameObject;
    // Start is called before the first frame update
    void Start()
    {
        attackPatternObject = enemyGameObject.transform.GetChild(5).gameObject;
        attackPattern = attackPatternObject.GetComponent<AttackPattern>();
        playerCombo = playerScript.Combo;
        stagePlayTime = 0;
        deffenceScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerScript.Combo > playerCombo)
        {
            playerComboScore++;
            playerCombo = playerScript.Combo;
        }
        if (playerScript.Combo == 0)
        {
            if (maxCombo < playerCombo)
            {
                maxCombo = playerCombo;
            }
            playerCombo = 0;
        }

        if (enemyScript.Hp >= 0)
        {
            stagePlayTime += Time.deltaTime;
        }
        else
        {
            resultGameObject.SetActive(true);

            attackScoreText.text = "AttackPoint :" + playerComboScore;


        }
        if (attackPattern.CounterSuccesed)
        {
            deffenceScore++;
        }
    }

    public int GetScoreCombo()
    {
        return playerComboScore;
    }
    public int GetMaxCombo()
    {
        return maxCombo;
    }

    public float GetPlayTime()
    {
        return stagePlayTime;
    }
}
