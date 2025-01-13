using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
// ScriptableObject for animation data
[CreateAssetMenu(fileName = "NewAnimationData", menuName = "Animation/AnimationData")]
public class ClothesData : ScriptableObject
{
    public Sprite animationImage;
    public string title;
    public string description;
    public string price;
    public string buyNowURL;
}

// Script to manage the popup UI
public class AnimationPopup : MonoBehaviour
{
    [Header("UI Elements")]
    public Image animationImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public Button closeButton;
    public Button buyNowButton;
    public ClothesData animationData;

    public CollectibleManager collectibleManager;

    [Header("Popup Settings")]
    public GameObject popupWindow;

    [Header("Animation Settings")]
    public float entranceDuration = 0.5f;
    public Vector3 entranceScale = Vector3.one;
    public Vector3 entranceStartScale = new Vector3(0.1f, 0.1f, 0.1f);

    public float exitDuration = 0.5f;
    public Vector3 exitScale = new Vector3(0.1f, 0.1f, 0.1f);



    private void Start()
    {

        // Ensure popup is hidden initially
        //popupWindow.SetActive(false);
        popupWindow.transform.localScale = entranceStartScale;

        // Add listeners to buttons
        closeButton.onClick.AddListener(ClosePopup);
        buyNowButton.onClick.AddListener(() => OpenBuyURL());
    }

    // Function to populate the popup with data
    public void ShowPopup(ClothesData animationData)
    {
        animationImage.sprite = animationData.animationImage;
        titleText.text = animationData.title;
        //descriptionText.text = animationData.description;
        priceText.text = animationData.price;

        buyNowButton.onClick.RemoveAllListeners();
        buyNowButton.onClick.AddListener(() => OpenBuyURL(animationData.buyNowURL));

        popupWindow.SetActive(true);
        StartCoroutine(PlayEntranceAnimation());
    }

    // Close the popup window
    private void ClosePopup()
    {
        collectibleManager.SetBlackAndWhite(false);
        StartCoroutine(PlayExitAnimation());

    }

    // Open the Buy Now URL
    private void OpenBuyURL(string url = "")
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
    }



    // Coroutine for entrance animation
    private System.Collections.IEnumerator PlayEntranceAnimation()
    {
        float elapsedTime = 0f;
        popupWindow.transform.localScale = entranceStartScale;

        while (elapsedTime < entranceDuration)
        {
            popupWindow.transform.localScale = Vector3.Lerp(entranceStartScale, entranceScale, elapsedTime / entranceDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        popupWindow.transform.localScale = entranceScale;
    }

    // Coroutine for exit animation
    private System.Collections.IEnumerator PlayExitAnimation()
    {
        float elapsedTime = 0f;
        Vector3 startScale = popupWindow.transform.localScale;

        while (elapsedTime < exitDuration)
        {
            popupWindow.transform.localScale = Vector3.Lerp(startScale, exitScale, elapsedTime / exitDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        popupWindow.transform.localScale = exitScale;
        popupWindow.SetActive(false);
    }
}
