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

    [SerializeField] private GameObject _titleButtonObj;
    [SerializeField] private GameObject _subtitleButtonObj;

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
        buttons = new GameObject[_newsButtonParameters.Length];
        content = scrollRect.content;

        startPositions = new Vector2[_newsButtonParameters.Length];
        targetPositions = new Vector2[_newsButtonParameters.Length];

        totalWidth = 0f;

        for (int i = 0; i < _newsButtonParameters.Length; i++)
        {
            Debug.Log(i);
            buttons[i] = Instantiate(buttonPrefab, scrollRect.gameObject.transform.position, Quaternion.identity, content.transform);
            buttons[i].name = "Button : " + i;
            buttons[i].GetComponent<Image>().sprite = _newsButtonParameters[i]._background;

            GameObject childTitleObj = buttons[i].transform.Find("Title")?.gameObject;
            GameObject childSubTitleObj = buttons[i].transform.Find("Subtitle")?.gameObject;

            TextMeshProUGUI _title = childTitleObj.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI _subtitle = childSubTitleObj.GetComponent<TextMeshProUGUI>();
            _title.text = _newsButtonParameters[i]._title;
            _subtitle.text = _newsButtonParameters[i]._subtitle;

            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(totalWidth, 0);

            startPositions[i] = buttonRect.anchoredPosition;
            totalWidth += buttonRect.rect.width;
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

            // Récupération du bouton à déplacer à la fin
            int buttonToMoveIndex = currentNews - 1;
            RectTransform buttonToMoveEnd = buttons[buttonToMoveIndex].GetComponent<RectTransform>();

            // Placer immédiatement le bouton à la fin du carrousel
            buttonToMoveEnd.anchoredPosition = new Vector2(totalWidth, 0);

            // Mise à jour des positions de départ après repositionnement
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
}
