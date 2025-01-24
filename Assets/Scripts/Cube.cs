using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    public Rigidbody Rigidbody;

    public bool IsStart { get; private set; }

    public event Action<Cube> Released;

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        IsStart = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Platform>(out Platform platform))
        {
            if (IsStart == false)
            {
                MeshRenderer.material.color = UnityEngine.Random.ColorHSV();
                float time = platform.GetLiveTime();

                StartCoroutine(StartTimer(time));
            }
        }
    }

    private IEnumerator StartTimer(float time)
    {
        IsStart = true;
        yield return new WaitForSeconds(time);
        Released?.Invoke(this);  
    }
}
