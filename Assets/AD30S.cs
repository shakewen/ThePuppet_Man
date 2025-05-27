using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD30S : MonoBehaviour
{
    
    private void Start()
    {
        //StartCoroutine(AD30sStart());
    }

    IEnumerator AD30sStart()
    {
        yield return new WaitForSeconds(30f);
        AdManager1.instance.ShowYuanSheng(true);
    }
}
