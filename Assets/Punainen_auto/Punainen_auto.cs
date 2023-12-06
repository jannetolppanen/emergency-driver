using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punainen_auto : MonoBehaviour
{
    private testiajelu script;
    private GameObject brakeLight;

    void Start()
    {
        script = GetComponent<testiajelu>();
        brakeLight = transform.Find("Jarruvalo").gameObject;
    }

    void Update()
    {
        if(script.PlayerBack)
        {
            brakeLight.SetActive(true);
        }
        else
        {
            brakeLight.SetActive(false);
        }
    }
}
