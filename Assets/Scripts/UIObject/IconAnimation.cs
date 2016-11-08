using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Icon/Animations")]
public class IconAnimation : MonoBehaviour {
    public Sprite[] spriteAnims;
    public float delay;
    public float duration;
    Image img;
    // Use this for initialization
    void Start() {
        img = GetComponent<Image>();
        InvokeRepeating("animSprite", delay, duration);
    }

    int index = 0;
    void animSprite() {
        img.sprite = spriteAnims[index];
        index++;
        if (index >= spriteAnims.Length)
            index = 0;
    }
}
