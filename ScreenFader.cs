using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenFader : MonoBehaviour 
{
    public enum FadeType { FadeIn, FadeOut };

    public FadeType type;
    public float fadeTime = 2.0f;
    public float waitTime = 1.0f; // Time to wait after fading is finished before resetting.
    public bool startImmediately = true;
    public bool startOnTrigger = false;
    public bool resetWhenComplete = false;
    public Texture imageFade;
    public string playerGameObjectName = "FPSPlayer";

    private float startTime;
    private float currentTime;
    private bool running = false;

	// Use this for initialization
	void Start () 
    {
		if (startImmediately)
        {
            StartFade();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void StartFade()
    {
        running = true;
        startTime = Time.time;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == playerGameObjectName)
        {
            StartFade();
        }
    }

    void OnGUI()
    {
        currentTime = Time.time;

        if (running)
        {
            float progress = (currentTime - startTime) / fadeTime;
            Color newColor = GUI.color;

            if (type == FadeType.FadeIn)
            {
                newColor.a = Mathf.Clamp01(1 - progress);
            }
            else
            {
                newColor.a = Mathf.Clamp01(progress);
            }

            // This currently draws over everything in every camera.
            GUI.color = newColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), imageFade);
        }

        if (resetWhenComplete && startTime + fadeTime + waitTime <= currentTime)
        {
            running = false;
            return;
        }
    }
}
