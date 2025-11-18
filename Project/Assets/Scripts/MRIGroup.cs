using UnityEngine;

public class MRIGroup : MonoBehaviour
{
    [SerializeField] private ShowMessagesController messageController;


    private void Start()
    {
        //StartSim();
    }


    private void StartSim()
    {
        messageController.ShowMessageAtIndex(0);
    }
}
