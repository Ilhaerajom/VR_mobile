using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RonnieJ.Coroutine
{
    public class Fadeinout : MonoBehaviour
    {
        public float animTime = 2f;
        private Image fadeImage;
        private float start = 0f;
        private float end = 1f;
        private float time = 0f;
        private bool isPlaying = false;
        void Awake()
        {
            fadeImage = GetComponent<Image>();
        }
        public void StartFadeAnim()
        {
            if (isPlaying == true)
                return;
            StartCoroutine("PlayFadeOut");
        }
        IEnumerator PlayFadeOut()
        {
            Color color = fadeImage.color;
            time = 0f;
            color.a = Mathf.Lerp(start, end, time);
            while (color.a < 1f)
            {
                time += Time.deltaTime / animTime;
                color.a = Mathf.Lerp(start, end, time);
                fadeImage.color = color;
                yield return null;
            }
            isPlaying = false;

        }
    }
}
