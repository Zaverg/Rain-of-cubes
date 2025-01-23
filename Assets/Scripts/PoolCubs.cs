using UnityEngine;

public class PoolCubs : Pool<Cube>
{
    [SerializeField] private Cube _prefab;

    public override Cube GetObject()
    {
        Cube obj = base.GetObject();
        obj.Rigidbody.isKinematic = false;

        return obj;
    }

    public override void OnRelease(Cube obj)
    {
        obj.MeshRenderer.material.color = _prefab.MeshRenderer.sharedMaterial.color;
        obj.transform.rotation = _prefab.transform.rotation;
        obj.Rigidbody.isKinematic = true;
        base.OnRelease(obj);
    }
}
