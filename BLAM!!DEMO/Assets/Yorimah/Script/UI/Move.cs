using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI
{
    public class Move:MonoBehaviour
    {
        [SerializeField,Header("UI")]
        GameObject instantieatedCard;
        public void Start()
        {
            StartCoroutine(MoveCard());
        }

        // カードを指定された位置に移動させるコルーチン
        private IEnumerator MoveCard()
        {
            Vector3 StartDeckPos = new Vector3(0f, 0f, 0f);
            Vector3 EndHandPos = new Vector3(-100f, -100f, 0f);
            float animDuration = 1f; // アニメーションの総時間
            float startTime = Time.time;

            while (Time.time - startTime < animDuration)
            {
                float journeyFraction = (Time.time - startTime) / animDuration;
                //滑らかに移動させるさせる場合は以下のコード追加する
                journeyFraction = Mathf.SmoothStep(0f, 1f, journeyFraction);
                instantieatedCard.transform.localPosition = Vector3.Lerp(StartDeckPos, EndHandPos, journeyFraction);
                yield return null;
            }
        }
    }
}

