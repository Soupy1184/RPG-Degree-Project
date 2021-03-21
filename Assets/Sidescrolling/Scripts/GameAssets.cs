using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{

     private static GameAssets _i;

     public static GameAssets i {
          get {
               if (_i == null) {
                    //instantiates resources globally in the scene so they can be used in other functions (in this case, the prefabDamagePopup)
                    _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
               }
               return _i;
          }
     }

     public Transform prefabDamagePopup;
}
