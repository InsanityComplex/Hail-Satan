using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Countdown : MonoBehaviour {

    public float MaxTime;
    public PostProcessVolume Post;
    public GameObject[] Candles;
    public bool Bleeding;

    int currentObject;
    float vignetteTimer;
    float abberationTimer;
    float timer;
    ChromaticAberration abberation;
    Vignette vignette;

    private void Start()
    {
        abberation = ScriptableObject.CreateInstance<ChromaticAberration>();
        vignette = ScriptableObject.CreateInstance<Vignette>();
        abberation.enabled.Override(true);
        vignette.enabled.Override(true);

        Post = PostProcessManager.instance.QuickVolume(8, 100f, abberation, vignette);
        Post.isGlobal = true;
    }

    void Update () {
        
        timer += Time.deltaTime;
        abberationTimer += Time.deltaTime;
        vignetteTimer += (Bleeding) ? Time.deltaTime : 0;
        abberation.intensity.Override(abberationTimer / MaxTime);
        vignette.intensity.Override(vignetteTimer / 10);

        if (timer >= MaxTime / 3)
        {
            Candles[currentObject].SetActive(false);
            currentObject++;

            timer = 0;
        }

        if (currentObject >= Candles.Length || vignetteTimer > 10)
        {
            //Game over.
        }
	}
}
