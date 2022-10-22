using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class AudioVisualiser : MonoBehaviour
{
    public static float[] musicSamples = new float[512];
    public static float[] frequencyBands = new float[8];
    public static float difference = 0;
    private AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
    }

    private void GetSpectrumAudioSource()
    {
        music.GetSpectrumData(musicSamples, 0, FFTWindow.Blackman);
    }

    private void MakeFrequencyBands()
    {
        // Bands ranges - 0-7 - 2,4,8,16,32,64,128,258
        float average = 0;
        int count = 0;
        float min = 0;
        float max = 0;
        
        for (int i = 0; i < 8; i++)
        {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                // Get min and max
                if (musicSamples[count] < min)
                {
                    min = musicSamples[count];
                }
                if (musicSamples[count] > max)
                {
                    max = musicSamples[count];
                }

                average += musicSamples[count] * count + 1;
                count++;
            }

            difference = max - min;

            average /= count;

            frequencyBands[i] = average;
        }
    }
}
