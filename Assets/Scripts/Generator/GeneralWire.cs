using UnityEngine;

public class GeneralWire : ModelRotate
{
    [SerializeField] private Material[] firstMaterial;
    [SerializeField] private Material[] secondMaterial;
    [SerializeField] private Material[] thirdMaterial;

    public void AddPower(int powerValue)
    {
        switch (powerValue)
        {
            case 1:
            {
                foreach (Material mat in firstMaterial)
                {
                    mat.SetColor("_Color", Color.yellow);
                }

                break;
            }
            case 2:
            {
                foreach (Material mat in secondMaterial)
                {
                    mat.SetColor("_Color", Color.yellow);
                }

                break;
            }
            case 3:
            {
                foreach (Material mat in thirdMaterial)
                {
                    mat.SetColor("_Color", Color.yellow);
                }

                EventManager.Publish("ApplyPower");
                break;
            }
        }
    }
}