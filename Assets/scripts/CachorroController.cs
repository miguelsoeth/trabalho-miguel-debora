using UnityEngine;

public class CachorroController : PlayerController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Latir();
        }
    }

    void Latir()
    {
        Debug.Log("Cachorro latiu!");
    }
}
