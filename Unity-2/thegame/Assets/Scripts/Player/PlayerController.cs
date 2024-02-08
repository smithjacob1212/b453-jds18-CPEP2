using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.UI;
using MilkShake;

using FMODUnity;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(PingPongColor))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(UnityEngine.Rendering.Universal.Light2D))]
public class PlayerController : MonoBehaviour
{
    public ColorEnum color = ColorEnum.black;
    public int playerIndex;
    public StudioEventEmitter switchColor;
    public int colorIndex = 0;
    public int previousColorIndex = 0;
    public bool hasNotUnlockColor = true, hasUnlockPink = false, hasUnlockCyan = false, hasUnlockYellow = false;
    private UnityEngine.Rendering.Universal.Light2D light2D;
    public PingPongColor pingPongColor;
    public Animator animator;
    public float dashSpeed = 1f;
    public float stuntTime = 1f;
    public bool isStunt = false;
    [SerializeField]
    public bool jumped = false;
    public bool jumpCanceled = false;
    public float jumpCooldown = 0.2f;
    public bool dash = false;
    public bool isPunching = false;
    public bool isDodging = false;
    public float dodgeTime = 0.5f;
    private CharacterController2D controller;
    private SpriteRenderer sprite;
    [SerializeField]
    private float playerSpeed = 40.0f;
    private Vector2 playerVelocity;
    public PlayerDeath playerDeath;
    [SerializeField]
    private Sprite pinkSprite,cyanSprite,blackSprite, whiteSprite, orangeSprite;
    [SerializeField]
    private Color pink,cyan,black, white, orange;

    private void SwitchColorSprite(ColorEnum color)
    {
       switch (color)
        {
            case ColorEnum.pink:
                SwitchToPink();
                break;
            case ColorEnum.cyan:
                SwitchToCyan();
                break;
            case ColorEnum.orange:
                SwitchToYellow();
                break;
                /* Not managed anymore
            case ColorEnum.black:
                this.color = ColorEnum.black;
                sprite.sprite = blackSprite;
                light2D.color = black;
                break;
            case ColorEnum.white:
                this.color = ColorEnum.white;
                sprite.sprite = whiteSprite;
                light2D.color = white;
                break;
            
                */
        }
    }
    private void SwitchToCyan()
    {
        RuntimeManager.PlayOneShot(switchColor.EventReference);
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (light2D == null) light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        colorIndex = 1;
        controller.SetLayerWeight(previousColorIndex, colorIndex, 1);
        this.color = ColorEnum.cyan;
        sprite.sprite = cyanSprite;
        light2D.color = cyan;
    }
    private void SwitchToPink()
    {
        RuntimeManager.PlayOneShot(switchColor.EventReference);
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (light2D == null) light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        colorIndex = 0;
        controller.SetLayerWeight(previousColorIndex, colorIndex, 1);
        this.color = ColorEnum.pink;
        sprite.sprite = pinkSprite;
        light2D.color = pink;
    }

    private void SwitchToYellow()
    {
        RuntimeManager.PlayOneShot(switchColor.EventReference);
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (light2D == null) light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        colorIndex = 2;
        controller.SetLayerWeight(previousColorIndex, colorIndex, 1);
        this.color = ColorEnum.orange;
        sprite.sprite = orangeSprite;
        light2D.color = orange;
    }


    public void OnPressYellowColor(CallbackContext context)
    {
        if (!hasUnlockYellow) return;
        SwitchToYellow();
    }

    public void OnPressCyanColor(CallbackContext context)
    {
        if (!hasUnlockCyan) return;
        SwitchToCyan();
    }

    public void OnPressPinkColor(CallbackContext context)
    {
        if (!hasUnlockPink) return;
        SwitchToPink();
    }


    public void UnlockColor(ColorEnum colorToUnlock)
    {
        if (controller == null) controller = gameObject.GetComponent<CharacterController2D>();
        hasNotUnlockColor = false;
        switch (colorToUnlock)
        {
            case ColorEnum.pink:
                hasUnlockPink = true;
                SwitchColorSprite(colorToUnlock);
                break;
            case ColorEnum.cyan:
                hasUnlockCyan = true;
                SwitchColorSprite(colorToUnlock);
                break;
            case ColorEnum.orange:
                hasUnlockYellow = true;
                SwitchColorSprite(colorToUnlock);
                break;
        }
    }
    /// <summary>
    /// i is the left (-1) or right (+1)
    /// </summary>
    /// <param name="i"></param>
    /// <param name="colorIndex"></param>
    private void TransformIndexColorToColor(int i, int colorIndex)
    {
       switch (colorIndex)
        {
            case 0:
                if (!hasUnlockPink)
                {
                    ChangeColorIndex(i);
                    return;
                }
                color = ColorEnum.pink;
                break;
            case 1:
                if (!hasUnlockCyan)
                {
                    ChangeColorIndex(i);
                    return;
                }
                color = ColorEnum.cyan;
                break;
            case 2:
                if (!hasUnlockYellow)
                {
                    ChangeColorIndex(i);
                    return;
                }
                color = ColorEnum.orange;
                break;
            default:
                throw new Exception("Bad color index !! " + colorIndex);
        }
        SwitchColorSprite(color);
       
    }
    /// <summary>
    /// i = -1 => left color
    /// i = +1 => right color
    /// </summary>
    /// <param name="i"></param>
    private void ChangeColorIndex(int i)
    {
        if (hasNotUnlockColor)
        {
            NoColorUnlocked();
            return;
        }
        previousColorIndex = colorIndex;
        colorIndex += i;
        if (colorIndex > 2)
        {
            colorIndex = 0;
        }
        if (colorIndex  < 0)
        {
            colorIndex = 2;
        }
        TransformIndexColorToColor(i, colorIndex);
    }

    public void NoColorUnlocked()
    {
        Debug.Log("No Color unlocked yet");
    }

    public void ManageObstacleColor(ColorEnum obstacleColor)
    {
        if (obstacleColor == ColorEnum.black) return;
        if (obstacleColor != this.color)
        {
            playerDeath.Die();
        }
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }
    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
    }

    public void OnJump(CallbackContext context)
    {
        context.action.performed += ctx => jumped = true;
        jumpCanceled = context.action.phase == InputActionPhase.Canceled;
    }
    public void OnDieButton(CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            playerDeath.Die();
        }
    }

    public void OnMove(CallbackContext context)
    {
        playerVelocity = context.ReadValue<Vector2>();
    }

    public void OnDash(CallbackContext context)
    {
        dash = context.action.phase == InputActionPhase.Performed;
    }

    public void OnLeftColor(CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed) {
            ChangeColorIndex(-1);
        }
    }

    public void OnRightColor(CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            ChangeColorIndex(1);
        }
    }

    private void FixedUpdate()
    {
        float speed = isStunt ? 0 : playerSpeed;
        float dashCoef = dash ? dashSpeed : 1f;
        controller.Move(playerVelocity.x * speed * dashCoef * Time.fixedDeltaTime, false, jumped, jumpCanceled);
        jumped = false;
    }

    internal void Stunt()
    {
        if (isStunt) return;
        StartCoroutine(IsStunt());
    }

    private IEnumerator IsStunt()
    {
        isStunt = true;
        pingPongColor.StartPingPong(new Color(0, 0, 0, 0), stuntTime);
        while (pingPongColor.isFlashing)
        {
            jumped = false;
            yield return null;
        }
        isStunt = false;
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController2D>();
        sprite = GetComponent<SpriteRenderer>();
        pingPongColor = GetComponent<PingPongColor>();
        playerDeath = GetComponent<PlayerDeath>();
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        if (hasNotUnlockColor)
        {
            controller.SetLayerWeight(previousColorIndex, 3, 1);
        }
    }
}

public enum ColorEnum
{
    pink,
    cyan,
    orange,
    black,
    white
}