using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (PlayerController.Instance() == null)
        {
            Instantiate(Player);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
