using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour {

    int randomFunc;
    int randomOffset;
    Light light;

	void Start () {
        light = GetComponent<Light>();
        randomFunc = Random.Range(0, 3);
        randomOffset = Random.Range(0, 100);
	}
	
	// Update is called once per frame
	void Update () {
        float intensity;
        
        switch (randomFunc)
        {
            case 0:
                intensity = Mathf.Clamp(Mathf.Sin(Time.timeSinceLevelLoad + randomOffset / 5) + 2, 0.5f, 1.5f);
                break;
            case 1:
                intensity = Mathf.Clamp(Mathf.Sin(Time.timeSinceLevelLoad + randomOffset / 5), 0.5f, 1.5f);
                break;
            case 2:
                intensity = Mathf.Clamp(Mathf.Clamp01(Mathf.Cos(Time.timeSinceLevelLoad + randomOffset / 5) + Mathf.Sin(Time.timeSinceLevelLoad + randomOffset / 5) + 2), 0.5f, 1.5f);
                break;
            default:
                intensity = 0;
                break;
        }
        light.intensity = intensity + Random.Range(0, 2f) / 25;
    }
}
