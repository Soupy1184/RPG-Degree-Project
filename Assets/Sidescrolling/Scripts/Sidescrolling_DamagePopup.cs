
// Coded using Tutorial Youtube Video: https://www.youtube.com/watch?v=iD1_JczQcFY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sidescrolling_DamagePopup : MonoBehaviour
{
     //Creates the damage popup
     public static Sidescrolling_DamagePopup Create(Vector3 position, int damage, bool isCriticalHit) {
          Transform damagePopupTransform = Instantiate(GameAssets.i.prefabDamagePopup, position, Quaternion.identity);
           
          Sidescrolling_DamagePopup damagePopup = damagePopupTransform.GetComponent<Sidescrolling_DamagePopup>();
          damagePopup.Setup(damage, isCriticalHit);

          return damagePopup;
      }

     private static int sortingOrder;

     private const float DISAPPEAR_TIMER_MAX = 0.7f;

     private TextMeshPro textMesh;
     private float disappearTimer;
     private Color textColor;

    private void Awake()
    {
          textMesh = transform.GetComponent<TextMeshPro>();
    }

     public void Setup(int damageAmount, bool isCriticalHit) {
          textMesh.SetText(damageAmount.ToString());
          if (!isCriticalHit) {
               // Not a critical hit (normal hit) means smaller text and lighter yellow color
               textMesh.fontSize = 4;
               textColor = new Color32(255, 197, 0, 255);
          }
          else {
               // Critical Hit means larger text and darker red color
               textMesh.fontSize = 6;
               textColor = new Color32(255, 43, 0, 255);
          }
          textMesh.color = textColor;
          disappearTimer = DISAPPEAR_TIMER_MAX;

          sortingOrder++;
          textMesh.sortingOrder = sortingOrder;
     }



     private void Update() {
          float moveYSpeed = 3f;
          transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

          if(disappearTimer > DISAPPEAR_TIMER_MAX * .9f) {
               //first half of popup here, so get bigger
               float increaseScaleAmount = 5f;
               transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
          }
          else {
               //second half of popup here, so get smaller
               float decreaseScaleAmount = 1f;
               transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
          }

          disappearTimer -= Time.deltaTime;
          if (disappearTimer < 0) {
               // once timer elapses, start disappearing
               float disappearSpeed = 3f;
               textColor.a -= disappearSpeed * Time.deltaTime;
               textMesh.color = textColor;
               if (textColor.a < 0) {
                    Destroy(gameObject);
               }
          }
     }

}
