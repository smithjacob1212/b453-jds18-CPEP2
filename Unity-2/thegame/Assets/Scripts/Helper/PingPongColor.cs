using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PingPongColor : MonoBehaviour
{
    private SpriteRenderer sprite;
    public bool isFlashing = false;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    //#############################################################//
    //######### Flashes for Player Invulnerability Frames #########//
    //#############################################################//
    private IEnumerator Flashes(
        Color toColor,
        Color fromColor,
        float flashDuration = 1f,
        float timeBetweenTwoFlashes = 0.02f
    )
    {
        if (toColor == null)
        {
            toColor = new Color(0f, 0f, 0f, 0f);
        }
        //Thanks to https://www.youtube.com/watch?v=phZRfEAuW7Q
        isFlashing = true;
        float timer = flashDuration;
        while (timer > 0f)
        {
            ToColor(toColor);
            yield return new WaitForSeconds(timeBetweenTwoFlashes);
            FromColor(fromColor);
            yield return new WaitForSeconds(timeBetweenTwoFlashes);
            timer -= timeBetweenTwoFlashes * 2;
        }
        FromColor(fromColor);
        isFlashing = false;
    }

    private void FromColor(Color fromColor)
    {
        sprite.color = fromColor;
    }

    private void ToColor(Color toColor)
    {
        sprite.color = toColor;
    }

    public void StartPingPong(
        Color toColor,
        float flashDuration = 1f,
        float timeBetweenTwoFlashes = 0.02f
    )
    {
        StartCoroutine(
            Flashes(
                toColor, 
                sprite.color, 
                flashDuration,
                timeBetweenTwoFlashes
            )
        );
    }

    //*************************************************//
    /***************** End of Flashes *****************/
    /**************************************************/
}
