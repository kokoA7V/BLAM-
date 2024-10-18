using UnityEngine;
using UnityEngine.UI;

public class ScoreObseber : MonoBehaviour
{
    [SerializeField, Header("���˂݁`")]
    private GameObject enemyGameObject;

    private Enemy enemyScript;

    private GameObject attackPatternObject;

    [SerializeField, Header("�Ղꂢ��`")]
    private Player playerScript;

    private int playerCombo;
    private int playerComboScore;

    private int maxCombo;

    private int deffenceScore;

    private float stagePlayTime;

    private AttackPattern attackPattern;

    [SerializeField, Header("�U���]���̃e�L�X�g")]
    private Text attackScoreText;

    [SerializeField, Header("���ԕ]���̃e�L�X�g")]
    private Text timeScoreText;

    [SerializeField, Header("�h��]���̃e�L�X�g")]
    private Text defennsedScoreText;

    [SerializeField, Header("�����]���̃e�L�X�g")]
    private Text summryScoreText;


    [SerializeField, Header("���U���g�̂܂Ƃ߃e�L�X�g")]
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
