using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string AreaToLoad;

    public string AreaTransitionName;

    public AreaEntrance entrance;

    public float TransitionTime = 1f;
    private bool shouldLoadAfterFade;

    // Start is called before the first frame update
    void Start()
    {
        entrance.TransitionName = AreaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade)
        {
            TransitionTime -= Time.deltaTime;

            if (TransitionTime <= 0f)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(AreaToLoad);
            }
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shouldLoadAfterFade = true;
            UIFade.Instance().FadeToBlack();

            PlayerController.Instance().AreaTransitionName = AreaTransitionName;

            GameManager.Instance().transitioningBetweenAreas = true;
        }
    }
}
