using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{
    public void EndAnimationMethod()
    {
        gameObject.SetActive(false);
        if (gameObject.name=="PauseCanvas")
        {
            Time.timeScale = 1;
        }        
    }
}
