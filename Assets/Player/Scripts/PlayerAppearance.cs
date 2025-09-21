using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class PlayerAppearance : MonoBehaviour
{

    [Header("Sprites")]
    [SerializeField] private Sprite redSprite;
    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite blueSprite;

    [Header("Audio")]
    [SerializeField] private AudioClip switchSound;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private PlayerRGBController rgbController;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rgbController = GetComponent<PlayerRGBController>();
    }

    private void OnEnable()
    {
        rgbController.OnColorChanged += HandleStateChange;
    }

    private void OnDisable()
    {
        rgbController.OnColorChanged -= HandleStateChange;
    }

    private void HandleStateChange(ColorState newState)
    {
        switch(newState)
        {
            case ColorState.Red:
                spriteRenderer.sprite = redSprite;
                break;
            case ColorState.Green: 
                spriteRenderer.sprite = greenSprite;
                break;
            case ColorState.Blue:
                spriteRenderer.sprite = blueSprite;
                break;
        }

        if (switchSound != null)
            audioSource.PlayOneShot(switchSound);
    }
}
