using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _timeLivesOfCubeMin;
    [SerializeField] private float _timeLivesOfCubeMax;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<Cube>(out Cube cube))
        {
            if (cube.IsStart == false)
            {
                cube.MeshRenderer.material.color = Random.ColorHSV();
                float time = Random.Range(_timeLivesOfCubeMin, _timeLivesOfCubeMax);
                cube.StartTimer(time);
            }
        }
    }
}
