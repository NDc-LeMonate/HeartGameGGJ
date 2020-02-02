using UnityEngine;
using UnityEngine.UI;

public class TextBlingBling : MonoBehaviour {

    Text text;
    float alphaValue;
    bool increase;

    public float minThres = 0, maxThres = 1;
    public float changeRate = 1;
    public enum StartAlphaValue { min,max,random}

    public StartAlphaValue startAlphaValue;

    private void Start()
    {
        text = GetComponent<Text>();

        if(startAlphaValue == StartAlphaValue.max)
        {
            alphaValue = 1f;
        }
        else if(startAlphaValue == StartAlphaValue.min)
        {
            alphaValue = 0f;
        }
        else
        {
            alphaValue = Random.Range(minThres, maxThres);
        }

    }

    void Update () 
    {
        if(!increase)
        {
            alphaValue -= changeRate * Time.deltaTime;
        }
        else
        {
            alphaValue += changeRate * Time.deltaTime;

        }
        if(alphaValue < minThres )
        {
            increase = true;
        }
        else if(alphaValue >maxThres)
        {
            increase = false;
        }
      


        text.color = new Color(text.color.r, text.color.g, text.color.b, alphaValue);
	}
}
