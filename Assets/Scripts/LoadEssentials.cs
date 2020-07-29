using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEssentials : MonoBehaviour
{
    public GameObject UICanvas;
    public GameObject Player;

    public GameObject gameManagerRef;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.Instance() == null)
        {
            Instantiate(Player);
        }

        if (UIFade.Instance() == null)
        {
            Instantiate(UICanvas);
        }

        if (GameManager.Instance() == null)
        {
            Instantiate(gameManagerRef);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
