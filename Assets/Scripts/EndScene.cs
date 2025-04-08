using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    [SerializeField] Image endPanel;
    // Start is called before the first frame update
    void Awake()
    {
      StartCoroutine(EndScreenFade());
    }
    private IEnumerator EndScreenFade()
    {
        yield return new WaitForSeconds(3f);
        endPanel.CrossFadeAlpha(0, 2, false);
    }
  
}
