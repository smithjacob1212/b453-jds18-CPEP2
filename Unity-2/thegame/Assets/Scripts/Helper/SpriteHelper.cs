using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteHelper
{
    // Start is called before the first frame update
    public static SpriteRenderer[] ChangeGameObjectSpritesColor(GameObject other, Color color)
    {
        SpriteRenderer[] sprites = GetAllGameobjectSprites(other);
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = color;
        }
        return sprites;
    }

    public static SpriteRenderer GetGameObjectSprite(GameObject gameObject)
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        }
        return sprite;
    }

    public static SpriteRenderer[] GetAllGameobjectSprites(GameObject other)
    {
        SpriteRenderer[] sprites;
        sprites = other.GetComponents<SpriteRenderer>();
        sprites = other.GetComponentsInChildren<SpriteRenderer>();
        return sprites;
    }

    public static void ColorSprites(SpriteRenderer[] sprites, Color color){
        foreach (SpriteRenderer sprite in sprites) {
            sprite.color = color;
        }
    }

    public static void ColorSprite(SpriteRenderer sprite, Color color){
        sprite.color = color;
    }

}

public static class RendererExtensions
{
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
