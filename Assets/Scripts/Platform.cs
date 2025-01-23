using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<Cube>(out Cube cube))
        {
            if (cube.IsStart == false)
            {
                cube.MeshRenderer.material.color = Random.ColorHSV();
                cube.StartTimer();
            }
        }
    }
}
