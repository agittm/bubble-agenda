using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text textContent;
    [SerializeField] private Button buttonNext;

    private bool isClicked;
    private int index;

    private void Awake()
    {
        buttonNext.onClick.AddListener(HandleOnClickNext);
    }

    private void OnDestroy()
    {
        buttonNext.onClick.RemoveListener(HandleOnClickNext);
    }

    public void Show(string[] contents, float delay = 0, System.Action OnFinished = null)
    {
        StartCoroutine(ShowProcess(contents, delay, OnFinished));
    }

    private IEnumerator ShowProcess(string[] contents, float delay = 0, System.Action OnFinished = null)
    {
        yield return new WaitForSeconds(delay);

        panel.SetActive(true);
        index = 0;
        Time.timeScale = 0;

        while (index < contents.Length)
        {
            textContent.text = contents[index];

            yield return new WaitUntil(() => isClicked);

            index++;
            isClicked = false;
        }

        panel.SetActive(false);
        Time.timeScale = 1;

        yield return null;

        OnFinished?.Invoke();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    private void HandleOnClickNext()
    {
        isClicked = true;
    }
}
