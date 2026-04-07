using HighlightPlus;
using System.Collections;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private float interval = .5f;


    private HighlightEffect HighlightEffect
    {
        get
        {
            if (_highlightEffect == null)
            {
                _highlightEffect = GetComponent<HighlightEffect>();
            }
            return _highlightEffect;
        }
    }
    private HighlightEffect _highlightEffect;


    private Coroutine highlightCoroutine;


    public void StartHighlight()
    {
        StopHighlight();

        highlightCoroutine = StartCoroutine(Method());

        IEnumerator Method()
        {
            while (true)
            {
                HighlightEffect.HitFX();

                yield return new WaitForSeconds(interval);
            }
        }
    }


    public void StopHighlight()
    {
        if (highlightCoroutine != null) StopCoroutine(highlightCoroutine); 
    }
}
