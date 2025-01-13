using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CollectibleManager : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    public ClothesData[] clothesDatas;

    public AnimationPopup popupWindow;
    public static Action<ClothesData> openTshirtPopup;


    private void OnEnable()
    {
        openTshirtPopup += OpenPopUp;

    }

    private void OnDisable()
    {
        openTshirtPopup -= OpenPopUp;
    }

    private void Start()
    {
        // Find all collectible items in the scene
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Cherry");

        for (int i =0; i < collectibles.Length; i++)
        {
            if (collectibles[i].GetComponent<CollectibleItem>() == null)
            {
                collectibles[i].AddComponent<CollectibleItem>();
                //if(clothesDatas.Length <= collectibles.Length)
                    collectibles[i].GetComponent<CollectibleItem>().clothesData = clothesDatas[i];

            }
        }

        // Get the ColorGrading effect from the Post Processing Volume
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out colorGrading);
        }
    }

    public void OpenPopUp(ClothesData clothesData)
    {
        popupWindow.ShowPopup(clothesData);
    }


    public void SetBlackAndWhite(bool isBlackAndWhite)
    {
        if (colorGrading != null)
        {
            colorGrading.saturation.value = isBlackAndWhite ? -100 : 0;


            if (isBlackAndWhite) 
            {
                StartCoroutine(LerpTimeScale(1, 0, 0.5f));
            }
            else
                StartCoroutine(LerpTimeScale(0, 1, 0.5f));


        }
    }


    private IEnumerator LerpTimeScale(float start, float end, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        Time.timeScale = end;
    }
}
