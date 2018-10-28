using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SatanReveal : MonoBehaviour {

    public float Duration;

    bool isEffectRunning;
    bool checkEffectRunning;
    Vignette v;
    ChromaticAberration ab;

    public void RevealSatan()
    {
        GetComponent<Animation>().Play();
        GameObject.Find("Countdown Timer").SendMessage("DisablePostProcessing");
        GameObject.Find("Countdown Timer").SetActive(false);
    }

    public void MessWithPostProcessing()
    {
        GetComponent<AudioSource>().Play();
        ab = ScriptableObject.CreateInstance<ChromaticAberration>();
        v = ScriptableObject.CreateInstance<Vignette>();
        ab.enabled.Override(true);
        v.enabled.Override(true);

        PostProcessVolume pv = PostProcessManager.instance.QuickVolume(8, 100f, ab, v);
        pv.isGlobal = true;
        checkEffectRunning = true;
        StartCoroutine(EffectCoroutine(v, ab, Duration));
    }

    public void Update()
    {
        if (!isEffectRunning && checkEffectRunning)
        {
            Application.Quit();
        }
    }

    IEnumerator EffectCoroutine(Vignette v, ChromaticAberration ab, float duration)
    {
        isEffectRunning = true;
        float timer = 0;
        while (timer < duration)
        {
            float vIntensity = v.intensity.GetValue<float>();

            ab.intensity.Override(Mathf.Sin(Time.timeSinceLevelLoad * 10));
            vIntensity = timer / duration;
            v.intensity.Override(vIntensity);
            timer += Time.deltaTime;
            if (vIntensity > .9f)
            {
                Camera.main.enabled = false;
                //play noise
            }
            yield return null;
        }
        isEffectRunning = false;
    }
}
