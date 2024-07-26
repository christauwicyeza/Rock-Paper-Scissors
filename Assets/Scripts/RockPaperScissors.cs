using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RockPaperScissorsGame : MonoBehaviour
{
    public Button RockButton;
    public Button PaperButton;
    public Button ScissorsButton;
    public Button ShootButton;
    public Button ReplayButton;
    public Button ExitButton;

    public GameObject Choices;
    public TMP_Text ResultText;

    private Choice playerChoice;
    private Choice computerChoice;
    private bool canChangeChoice = true;

    private enum Choice
    {
        Rock,
        Paper,
        Scissors
    }

    void Start()
    {
        RockButton.onClick.AddListener(() => OnChoiceSelected(Choice.Rock));
        PaperButton.onClick.AddListener(() => OnChoiceSelected(Choice.Paper));
        ScissorsButton.onClick.AddListener(() => OnChoiceSelected(Choice.Scissors));
        ShootButton.onClick.AddListener(OnShoot);
        ReplayButton.onClick.AddListener(ResetGame);
        ExitButton.onClick.AddListener(ExitGame);

        ResetGame();
    }

    void OnChoiceSelected(Choice choice)
    {
        if (!canChangeChoice) return;
        playerChoice = choice;
        Choices.SetActive(false);
    }

    public void OnShoot()
    {
        if (!canChangeChoice) return;

        canChangeChoice = false;
        ReplayButton.interactable = true;

        computerChoice = GetRandomChoice();
        ShowGameResult();
    }

    void ResetGame()
    {
        ResultText.text = "";
        ReplayButton.interactable = false;
        canChangeChoice = true;
        Choices.SetActive(true);
    }

    Choice GetRandomChoice()
    {
        return (Choice)Random.Range(0, 3);
    }

    void ShowGameResult()
    {
        string playerChoiceText = GetChoiceText(playerChoice);
        string computerChoiceText = GetChoiceText(computerChoice);

        string resultMessage;

        if (playerChoice == computerChoice)
        {
            resultMessage = "It's a Tie!";
        }
        else if ((playerChoice == Choice.Rock && computerChoice == Choice.Scissors) ||
                 (playerChoice == Choice.Paper && computerChoice == Choice.Rock) ||
                 (playerChoice == Choice.Scissors && computerChoice == Choice.Paper))
        {
            resultMessage = "You Win!";
        }
        else
        {
            resultMessage = "Computer Wins!";
        }

        ResultText.text = $"<b>You:       </b> {playerChoiceText} \n\n<b>vs</b> \n\n</b>Computer:        </b> {computerChoiceText}\n\n<b>{resultMessage}</b>";
    }

    string GetChoiceText(Choice choice)
    {
        switch (choice)
        {
            case Choice.Rock:
                return "<sprite=1>";
            case Choice.Paper:
                return "<sprite=2>";
            case Choice.Scissors:
                return "<sprite=0>";
            default:
                return "";
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
