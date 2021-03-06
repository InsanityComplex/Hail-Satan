﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBleed : MonoBehaviour {

    public Sprite BleedingSprite;
    public bool Bleeding;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "knife" && collision.relativeVelocity.magnitude > 2.5f)
        {
            GetComponent<SpriteRenderer>().sprite = BleedingSprite;
            transform.GetChild(0).gameObject.SetActive(true);
            Bleeding = true;
            GameObject.Find("Countdown Timer").GetComponent<Countdown>().Bleeding = true;
            GetComponent<AudioSource>().Play();
        }
    }
}
