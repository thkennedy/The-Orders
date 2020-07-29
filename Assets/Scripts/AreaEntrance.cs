using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string TransitionName;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.Instance().AreaTransitionName == TransitionName)
        {
            PlayerController.Instance().transform.position = transform.position;
        }

        UIFade.Instance().FadeToClear();
        GameManager.Instance().transitioningBetweenAreas = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
