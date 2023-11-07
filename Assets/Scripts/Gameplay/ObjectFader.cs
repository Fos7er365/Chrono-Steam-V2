using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    [SerializeField] float fadeSpeed, fadeAmount;
    float originalOpacity;
    Material mat;
    [SerializeField] bool isFading = false;

    public bool IsFading { get => isFading; set => isFading = value; }

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalOpacity = mat.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Fading? " + isFading);
        if (isFading) FadeObject();
        else ResetObjectFading();
    }

    void FadeObject()
    {
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
        mat.color = smoothColor;
    }

    void ResetObjectFading()
    {
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
        mat.color = smoothColor;
    }
}
