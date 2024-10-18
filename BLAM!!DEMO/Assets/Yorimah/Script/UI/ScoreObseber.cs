using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObseber : MonoBehaviour
{
    [SerializeField, Header("‚Õ‚ê‚¢‚â`")]
    private Player playerScript;

    private int playerCombo;
    private int playerComboScore;
    // Start is called before the first frame update
    void Start()
    {
        playerCombo = playerScript.Combo;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.Combo>playerCombo)
        {
            playerCombo++;
            playerCombo = playerScript.Combo;
            Debug.Log(playerCombo);
        }
    }
}
