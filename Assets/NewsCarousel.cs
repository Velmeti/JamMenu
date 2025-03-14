using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewsCarousel : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject buttonPrefab;
    public int numberOfButtons = 5;
    public float moveDuration = 1f;  // Dur�e du d�placement en secondes
    public float moveInterval = 5f;  // Intervalle entre chaque mouvement des boutons

    private RectTransform content;
    private GameObject[] buttons;
    private Vector2[] startPositions;
    private Vector2[] targetPositions;

    void Awake()
    {
        buttons = new GameObject[numberOfButtons];
        content = scrollRect.content;

        startPositions = new Vector2[numberOfButtons];
        targetPositions = new Vector2[numberOfButtons];

        float totalWidth = 0f;

        for (int i = 0; i < numberOfButtons; i++)
        {
            buttons[i] = Instantiate(buttonPrefab, scrollRect.gameObject.transform.position, Quaternion.identity, content.transform);

            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(totalWidth, 0);

            startPositions[i] = buttonRect.anchoredPosition;
            totalWidth += buttonRect.rect.width;
        }

        // Lancement du d�calage des boutons
        StartCoroutine(MoveButtonsPeriodically());
    }

    // Coroutine pour d�placer les boutons toutes les 5 secondes
    IEnumerator MoveButtonsPeriodically()
    {
        while (true)
        {
            // Pr�parer la position cible des boutons
            PrepareTargetPositions();

            // D�placement fluide des boutons pendant 1 seconde
            yield return StartCoroutine(MoveButtonsSmoothly());

            // Attente avant de d�placer � nouveau
            yield return new WaitForSeconds(moveInterval);
        }
    }

    // Pr�parer les positions cibles pour chaque bouton
    void PrepareTargetPositions()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
            targetPositions[i] = new Vector2(buttonRect.anchoredPosition.x - buttonRect.rect.width, 0);
        }

        // R�organiser les boutons pour que le premier bouton "passe" � la fin
        GameObject firstButton = buttons[0];
        for (int i = 0; i < buttons.Length - 1; i++)
        {
            buttons[i] = buttons[i + 1];
        }
        buttons[buttons.Length - 1] = firstButton;

        // Mettre � jour la position du premier bouton � la fin du carousel
        RectTransform firstButtonRect = firstButton.GetComponent<RectTransform>();
        float totalWidth = buttons[buttons.Length - 1].GetComponent<RectTransform>().anchoredPosition.x + firstButtonRect.rect.width;
        firstButtonRect.anchoredPosition = new Vector2(totalWidth, 0);
    }

    // D�placer les boutons en utilisant une interpolation fluide (Lerp) pendant 1 seconde
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

        // Assurez-vous que la position finale soit correcte (en cas de petites erreurs dues � la pr�cision de Lerp)
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
            buttonRect.anchoredPosition = targetPositions[i];
        }

        // Apr�s le mouvement, mettre � jour les positions de d�part pour le prochain mouvement
        for (int i = 0; i < buttons.Length; i++)
        {
            startPositions[i] = targetPositions[i];
        }
    }
}
