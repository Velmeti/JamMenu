using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering;
using TMPro;
using Unity.VisualScripting;

public class NewsCarousel : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject buttonPrefab;
    public float moveDuration = 1f;
    public float moveInterval = 5f;

    [SerializeField] private NewsButtonParameters[] _newsButtonParameters;

    private RectTransform content;
    private GameObject[] buttons;
    private Vector2[] startPositions;
    private Vector2[] targetPositions;

    private float totalWidth = 0f;
    private int currentNews = 0;

    private bool _canStartTimerReactivation = false;
    private float _timerReactivation = 0f;


    void Awake()
    {
        scrollRect.horizontal = false;
        scrollRect.vertical = false;
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

        buttons = new GameObject[_newsButtonParameters.Length];
        content = scrollRect.content;

        startPositions = new Vector2[_newsButtonParameters.Length];
        targetPositions = new Vector2[_newsButtonParameters.Length];

        totalWidth = 0f;

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
            buttonRect.anchoredPosition = new Vector2(totalWidth, 0);
            startPositions[i] = buttonRect.anchoredPosition;
            totalWidth += buttonRect.rect.width;

            string url = _newsButtonParameters[i]._url;
            Button buttonComponent = buttons[i].GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OpenWebsite(url));
            }
        }

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
            yield return new WaitForSeconds(moveInterval);

            if (currentNews < buttons.Length)
            {
                currentNews++;
            }
            else
            {
                currentNews = 1;
            }

            PrepareTargetPositions();

            yield return StartCoroutine(MoveButtonsSmoothly());

            Debug.Log("Fin du déplacement");

            int buttonToMoveIndex = currentNews - 1;
            RectTransform buttonToMoveEnd = buttons[buttonToMoveIndex].GetComponent<RectTransform>();

            buttonToMoveEnd.anchoredPosition = new Vector2(totalWidth - totalWidth / buttons.Length, 0);

            for (int i = 0; i < buttons.Length; i++)
            {
                startPositions[i] = buttons[i].GetComponent<RectTransform>().anchoredPosition;
            }

            // Désactiver temporairement le bouton et planifier sa réactivation
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
