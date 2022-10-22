using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [Range(0,7)]
    [SerializeField] private int frequencyBand;
    private int burstCount;
    private ParticleSystem.MainModule effectSystemMain;
    private ParticleSystem.EmissionModule effectSystemEmission;
    private float maxLifetime;

    // Start is called before the first frame update
    void Start()
    {
        effectSystemMain = GetComponent<ParticleSystem>().main;
        effectSystemEmission = GetComponent<ParticleSystem>().emission;
    }

    // Update is called once per frame
    void Update()
    {
        burstCount = (int)AudioVisualiser.frequencyBands[frequencyBand] * 25;
        maxLifetime = AudioVisualiser.difference * 100;

        effectSystemEmission.SetBurst(0, new ParticleSystem.Burst(0, burstCount));

        effectSystemMain.startLifetime = new ParticleSystem.MinMaxCurve(0.1f, maxLifetime);

    }
}
