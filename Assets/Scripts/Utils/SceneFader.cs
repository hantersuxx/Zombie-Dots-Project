﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField]
    private Image image;
    public Image Image => image;


    [SerializeField]
    private AnimationCurve curve;
    public AnimationCurve Curve => curve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
        Extensions.Log(GetType(), $"Fading to \"{scene}\"");
    }

    private IEnumerator FadeIn()
    {
        float time = 1f;

        while (time > 0f)
        {
            time -= Time.deltaTime;
            float alpha = Curve.Evaluate(time);
            Image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeOut(string scene)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime;
            float alpha = Curve.Evaluate(time);
            Image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        SceneManager.LoadScene(scene);
    }
}
