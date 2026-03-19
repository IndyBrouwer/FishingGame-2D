using UnityEngine;

public class FishScaleHelper : MonoBehaviour
{
    public float GetSpriteScale(CaughtFish caughtFish)
    {
        float referenceCm = 20f; 
        float referenceScale = 1.0f;

        float spriteScale = (caughtFish.size / referenceCm) * referenceScale;

        float minScale = 0.4f; //Smallest possible scale for the sprite
        float maxScale = 9f; //Biggest possible scale for the sprite

        spriteScale = Mathf.Clamp(spriteScale, minScale, maxScale);

        return spriteScale;
    }
}