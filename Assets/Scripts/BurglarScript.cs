using UnityEngine;
using UnityEngine.UI;

public class BurglarScript : MonoBehaviour
{
    private const string winTitle = "You win";
    private const string loseTitle = "You lose";
    private Vector3[] startPosition = new Vector3[3];

    private float currentTime = 0.0f;
    private int[] pinsResult = new int[3] { 5, 5, 5};
    private int[] pinsCurrent = new int[3];

    [SerializeField] private int[] FirstInstrument = new int[3];
    [SerializeField] private int[] SecondInstrument = new int[3];
    [SerializeField] private int[] ThirdInstrument = new int[3];
    [SerializeField] private float RoundCount;
    [SerializeField] private Text timerText;
    [SerializeField] private Text resultText;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject[] Pins;
    [SerializeField] private Image[] KeyImages = new Image[5];
    [SerializeField] private Sprite[] CommonKey = new Sprite[5];
    [SerializeField] private Sprite[] FinalKey = new Sprite[5];
    [SerializeField] private GameObject ResultPanel;

    void Start()
    {
        for(var i = 0; i< startPosition.Length; i++)
        {
            startPosition[i] = Pins[i].transform.position;
        }
        
        Reset();
    }

    public void Reset()
    {
        currentTime = RoundCount;
        timerText.text = RoundCount.ToString();

        for (var i = 0; i < pinsCurrent.Length; i++)
        {
            pinsCurrent[i] = Random.Range(0, 10);
            ValidatePin(pinsCurrent[i], i);
            Pins[i].transform.position = startPosition[i];
            Pins[i].transform.position += new Vector3(0, pinsCurrent[i] * 2, 0);
        }
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0.0f)
        {
            ShowResult(false);
            return;
        }

        timerText.text = currentTime.ToString(string.Format("f0"));
    }

    private void ShowResult(bool isWin)
    {
        GamePanel.SetActive(false);
        ResultPanel.SetActive(true);
        resultText.text = isWin ? winTitle : loseTitle;
    }

    public void ChangeFirstPin()
    {
        ChangePins(FirstInstrument);
    }

    public void ChangeSecondPin()
    {
        ChangePins(SecondInstrument);
    }

    public void ChangeThirdPin()
    {
        ChangePins(ThirdInstrument);
    }


    private void ChangePins(int[] array)
    {
        var validPins = 0;

        for (var i = 0; i < pinsCurrent.Length; i++)
        {
            var result = pinsCurrent[i] + array[i];

            if (result >= 0 && result < 10)
            {
                pinsCurrent[i] = result;
                Pins[i].transform.position += new Vector3(0, array[i]*2, 0);

                validPins += ValidatePin(pinsCurrent[i], i);
            }
        }

        if (validPins == pinsResult.Length)
        {
            ShowResult(true);
        }
    }

    private int ValidatePin(int pin, int index)
    {
        if (pin == pinsResult[index])
        {
            SwapImage(index, FinalKey);
            return 1;
        }
        else
        {
            SwapImage(index, CommonKey);
            return 0;
        }
    }

    private void SwapImage(int i, Sprite[] sprites)
    {
        switch (i)
        {
            case 0:
                KeyImages[0].sprite = sprites[0];
                KeyImages[1].sprite = sprites[1];
                break;
            case 1:
                KeyImages[2].sprite = sprites[2];
                break;
            case 2:
                KeyImages[3].sprite = sprites[3];
                KeyImages[4].sprite = sprites[4];
                break;
        }
    }
}
