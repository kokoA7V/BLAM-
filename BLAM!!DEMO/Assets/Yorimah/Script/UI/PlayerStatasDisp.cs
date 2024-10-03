using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatasDisp : MonoBehaviour
{
    [SerializeField, Header("Player")]
    private Player player;

    [SerializeField, Header("HPTEXT")]
    private Text hpText;

    [SerializeField, Header("SPTEXT")]
    private Text spText;
    
    [SerializeField, Header("COMBOTEXT")]
    private Text comboText;

    [SerializeField, Header("É|Å[ÉY")]
    private GameObject pauseObject;

    void Start()
    {
        hpText.text = "HP "+player.Hp;
        spText.text = "SP "+player.Sp;
        comboText.text = "COMBO "+player.Combo;
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "HP " + player.Hp;
        spText.text = "SP " + player.Sp.ToString("F0");
        comboText.text = "COMBO " + player.Combo;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
