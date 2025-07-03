using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitEffect", menuName = "Item/Accessories/Effect/OrbitEffect")]
public class OrbitEffect : AccessoriesEffect
{
    [Header("Orbit Effect")]
    [SerializeField] private GameObject orbitPrefab;
    [SerializeField] private float[] damage;

    private List<GameObject> orbitObjs = new List<GameObject>();

    protected override void OnEnable()
    {
        base.OnEnable();

        orbitObjs.Clear();
    }

    public override void Active1(Accessories accessories)
    {
        CreateOrbitObject(1);
    }

    public override void Active2(Accessories accessories)
    {
        CreateOrbitObject(2);
    }

    public override void Active3(Accessories accessories)
    {
        CreateOrbitObject(3);
    }

    public override void Active4(Accessories accessories)
    {
        CreateOrbitObject(4);
    }

    public override void Revoke(Accessories accessories)
    {
        foreach (var obj in orbitObjs)
        {
            if (obj != null)
                Destroy(obj);
        }
        orbitObjs.Clear();
    }

    private void CreateOrbitObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(orbitPrefab, Manager.Data.PassiveCon.orbitController.transform.position, Quaternion.identity,
                Manager.Data.PassiveCon.orbitController.transform);

            orbitObjs.Add(go);

            go.GetComponent<OrbitObject>().Init(damage[count]);
        }

        Manager.Data.PassiveCon.orbitController.Init(orbitObjs.ToArray());
    }
}
