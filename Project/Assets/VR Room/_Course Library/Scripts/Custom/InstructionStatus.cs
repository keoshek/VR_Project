using UnityEngine;

public class InstructionStatus : MonoBehaviour
{
    [SerializeField] private bool hasBeenCompleted;
    [SerializeField] private ShowMessageFromList messageDisplayer;


    public void StartInstruction()
    {
        if (!hasBeenCompleted) {
            messageDisplayer.ShowMessageAtIndex(0);
            gameObject.SetActive(true);
        }
    }


    public void FinishInstruction()
    {
        hasBeenCompleted = true;
        gameObject.SetActive(false);
    }
}
