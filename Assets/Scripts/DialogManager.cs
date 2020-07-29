using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private static DialogManager instance;
    public int currentLine;
    public GameObject dialogBox;

    public string[] dialogLines;
    public TextMeshProUGUI dialogText;
    public GameObject nameBox;
    public TextMeshProUGUI nameText;
    public GameObject moreTextImage;
    
    public static DialogManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (dialogBox.activeInHierarchy) 
            AdvanceText();
    }

    private void CheckName()
    {
        string nextDialog = dialogLines[currentLine];
        if (nextDialog.StartsWith("n-"))
        {
            if (nextDialog == "n-Player")
            {
                nameText.text = PlayerController.Instance().GetPlayerName();
            }
            else
            {
                nameText.text = dialogLines[currentLine].Substring(2);                
            }
            
            currentLine++;
        }
    }

    private void AdvanceText()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            currentLine++;
            
            if (currentLine >= dialogLines.Length)
                EndDialog();
            else
            {
                CheckName();
                dialogText.text = dialogLines[currentLine];
            }
                
        }
    }

    private void EndDialog()
    {
        dialogBox.SetActive(false);
        GameManager.Instance().dialogActive = false;
    }

    public void ShowDialog(string[] newLines, bool showNames)
    {
        currentLine = 0;
        dialogLines = newLines;
        dialogBox.SetActive(true);
        CheckName();
        dialogText.text = dialogLines[currentLine];
        
        GameManager.Instance().dialogActive = true;

        nameBox.SetActive(showNames);
    }
}