using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public Image FadeScreen;
    public float FadeSpeed;

    private bool shouldFadeToBlack = false;
    private bool shouldFadeToClear = false;

    public static UIFade Instance() { return instance; }
    private static UIFade instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, Mathf.MoveTowards(FadeScreen.color.a, 1f, FadeSpeed * Time.deltaTime));

            if (FadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeToClear)
        {
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, Mathf.MoveTowards(FadeScreen.color.a, 0, FadeSpeed * Time.deltaTime));

            if (FadeScreen.color.a == 0f)
            {
                shouldFadeToClear = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeToClear = false;

        FadeScreen.color = new Color(0, 0, 0, 0);
    }

    public void FadeToClear()
    {
        shouldFadeToClear = true;
        shouldFadeToBlack = false;

        FadeScreen.color = new Color(0, 0, 0, 1);
    }
}
