using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    [SerializeField] private bool correctAnswer;
    [SerializeField] private Image buttonImage;


    public void ProcessAnswer()
    {
        buttonImage.color = correctAnswer ? Color.green : Color.red;
    }
}
