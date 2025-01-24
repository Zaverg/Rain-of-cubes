using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _timeLivesOfCubeMin;
    [SerializeField] private float _timeLivesOfCubeMax;

    public float GetLiveTime() =>
        Random.Range(_timeLivesOfCubeMin, _timeLivesOfCubeMax);
}
