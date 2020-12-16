using UnityEngine;

//Fireball Games * * * PetrZavodny.com

[ExecuteInEditMode]
public class BorderCubeController : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private TopSelection topSelection = TopSelection.Anthenas;
    [SerializeField] private GameObject anthenas;
    [SerializeField] private GameObject lightPillar;
    [Header("Light Pillar Setup")]
    [SerializeField] [Range(0.0f, 359.9f)] private float BodyAngle = 0; 
    [SerializeField] [Range(-45.0f, 45.0f)] private float LightAngle = 0;
    [SerializeField] private Transform LightBody;
    [SerializeField] private Transform Light;
#pragma warning restore 649

    public enum TopSelection
    {
        Anthenas,
        LightPillar
    }

    void Start()
    {
        initialize();
    }

    void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            initialize();
        }
    }
    
    private void initialize()
    {
        switch (topSelection)
        {
            case TopSelection.Anthenas :
                anthenas.SetActive(true);
                lightPillar.SetActive(false);
                break;
            case TopSelection.LightPillar :
                anthenas.SetActive(false);
                lightPillar.SetActive(true);
                break;
        }
        
        LightBody.localRotation = Quaternion.Euler(0, BodyAngle,0);
        Light.localRotation = Quaternion.Euler(LightAngle, 0,0);
    }
}
