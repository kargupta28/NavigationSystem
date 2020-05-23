using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputController : MonoBehaviour
{

    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("XRI_Right_TriggerButton"))
            cube.SetActive(false);
        else if (Input.GetButtonUp("XRI_Right_TriggerButton"))
            cube.SetActive(true);
    }
}
