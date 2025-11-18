using UnityEngine;

public class PatientController : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform position;


    private GameObject patient;


    public void SpawnPatient()
    {
        patient = Instantiate(prefab, position.position, position.rotation);
    }
}
