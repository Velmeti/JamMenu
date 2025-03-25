using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering;
using TMPro;


public class NewsCarousel : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject buttonPrefab;
    public GameObject circlePrefab;
    public float moveDuration = 1f;
    public float moveInterval = 5f;

    [SerializeField] private NewsButtonParameters[] _newsButtonParameters;

    private RectTransform content;
    private GameObject[] buttons;
    private Vector2[] startPositions;
    private Vector2[] targetPositions;

    private GameObject[] _circles;
    private Sprite _baseCircleSprite;
    [SerializeField] private Sprite _selectCircleSprite;

    private float totalWidthButton = 0f;
    private float totalWidthCircle = 0f;
    [SerializeField] private float offsetCircleHorizontal = 5f;
    [SerializeField] private float offsetCircleVertical = 20f;

    private int currentNews = 0;

    private bool _canStartTimerReactivation = false;
    private float _timerReactivation = 0f;


    void Awake()
    {
        scrollRect.horizontal = false;
        scrollRect.vertical = false;
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

        buttons = new GameObject[_newsButtonParameters.Length];
        _circles = new GameObject[_newsButtonParameters.Length];
        content = scrollRect.content;
        _baseCircleSprite = circlePrefab.GetComponent<Image>().sprite;

        startPositions = new Vector2[_newsButtonParameters.Length];
        targetPositions = new Vector2[_newsButtonParameters.Length];

        totalWidthButton = 0f;

        for (int i = 0; i < _newsButtonParameters.Length; i++)
        {
            buttons[i] = Instantiate(buttonPrefab, scrollRect.gameObject.transform.position, Quaternion.identity, content.transform);
            buttons[i].name = "Button : " + i;
            buttons[i].GetComponent<Image>().sprite = _newsButtonParameters[i]._background;

            TextMeshProUGUI _title = buttons[i].transform.Find("Title")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI _subtitle = buttons[i].transform.Find("Subtitle")?.GetComponent<TextMeshProUGUI>();

            if (_title != null) _title.text = _newsButtonParameters[i]._title;
            if (_subtitle != null) _subtitle.text = _newsButtonParameters[i]._subtitle;

            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(totalWidthButton, 0);
            startPositions[i] = buttonRect.anchoredPosition;
            totalWidthButton += buttonRect.rect.width;

            string url = _newsButtonParameters[i]._url;
            Button buttonComponent = buttons[i].GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OpenWebsite(url));
            }
        }

        for (int y = 0; y < _newsButtonParameters.Length; y++)
        {
            _circles[y] = Instantiate(circlePrefab, scrollRect.gameObject.transform.position, Quaternion.identity, content.transform);

            RectTransform circleRect = _circles[y].GetComponent<RectTransform>();
            TextMeshProUGUI _subtitle = buttons[y].transform.Find("Subtitle")?.GetComponent<TextMeshProUGUI>();
            Vector2 _subtitlePosition = _subtitle.gameObject.GetComponent<RectTransform>().anchoredPosition;
            float buttonPrefabWidth = buttonPrefab.GetComponent<RectTransform>().rect.width;
            float buttonPrefabLeftBorder = buttonPrefabWidth - (buttonPrefabWidth / 2);
            circleRect.anchoredPosition = new Vector2(_subtitlePosition.x - buttonPrefabLeftBorder + totalWidthCircle, _subtitlePosition.y - offsetCircleVertical);
            totalWidthCircle += circleRect.rect.width + offsetCircleHorizontal;
        }

        _circles[0].GetComponent<Image>().sprite = _selectCircleSprite;

        StartCoroutine(MoveButtonsPeriodically());
    }



    private void Update()
    {
        if (_canStartTimerReactivation == true)
        {
            _timerReactivation += Time.deltaTime;
            if (_timerReactivation > moveDuration)
            {
                _canStartTimerReactivation = false;
                _timerReactivation = 0;
                buttons[currentNews - 1].SetActive(true);
            }

        }
    }


    IEnumerator MoveButtonsPeriodically()
    {
        while (true)
        {
            if (currentNews > 0 && currentNews < _circles.Length)
            {
                Debug.Log(currentNews);
                _circles[currentNews - 1].GetComponent<Image>().sprite = _baseCircleSprite;
                _circles[currentNews].GetComponent<Image>().sprite = _selectCircleSprite;
            }
            else if (currentNews >= _circles.Length)
            {
                _circles[_circles.Length - 1].GetComponent<Image>().sprite = _baseCircleSprite;
                _circles[0].GetComponent<Image>().sprite = _selectCircleSprite;
            }

            yield return new WaitForSeconds(moveInterval);

            if (currentNews < buttons.Length)
            {
                currentNews++;
            }
            else if (currentNews >= buttons.Length)
            {
                currentNews = 1;
            }

            PrepareTargetPositions();

            yield return StartCoroutine(MoveButtonsSmoothly());

            Debug.Log("Fin du déplacement");

            int buttonToMoveIndex = currentNews - 1;
            RectTransform buttonToMoveEnd = buttons[buttonToMoveIndex].GetComponent<RectTransform>();

            buttonToMoveEnd.anchoredPosition = new Vector2(totalWidthButton - totalWidthButton / buttons.Length, 0);

            for (int i = 0; i < buttons.Length; i++)
            {
                startPositions[i] = buttons[i].GetComponent<RectTransform>().anchoredPosition;
            }

            buttons[buttonToMoveIndex].SetActive(false);
            _canStartTimerReactivation = true;
        }
    }

    void PrepareTargetPositions()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
            targetPositions[i] = new Vector2(buttonRect.anchoredPosition.x - buttonRect.rect.width, 0);
        }
    }


    IEnumerator MoveButtonsSmoothly()
    {

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
                buttonRect.anchoredPosition = Vector2.Lerp(startPositions[i], targetPositions[i], elapsedTime / moveDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
            buttonRect.anchoredPosition = targetPositions[i];
        }




        for (int i = 0; i < buttons.Length; i++)
        {
            startPositions[i] = targetPositions[i];
        }

    }




    private void OpenWebsite(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
    }
}
