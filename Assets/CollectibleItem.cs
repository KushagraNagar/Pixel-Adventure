using System.Collections;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private bool isPopupActive = false;
    private CollectibleManager collectibleManager;
    public ClothesData clothesData;

    private void Start()
    {
        collectibleManager = FindObjectOfType<CollectibleManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collectibleManager.SetBlackAndWhite(true);
            CollectibleManager.openTshirtPopup?.Invoke(clothesData);
        }
    }

}
