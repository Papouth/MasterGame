using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Osmose_Transition : MonoBehaviour
{
    private float time;
    private float ratio;

    public int limite;

    public Material onde;
    public Material cadrillage;
    public Material highlight;

    public GameObject object_volume;
    private Volume volume;

    // Start is called before the first frame update
    void Start()
    {
        ratio = 0;
        onde.SetFloat("_Effect_Opacity", 0);
        cadrillage.SetFloat("_Scan_Opacity", 0);
        highlight.SetFloat("Highlight_Opacity", 0);

        volume = object_volume.GetComponent<Volume>();
        volume.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (ratio < 1)
        {
            ratio = time/limite;
        }
        else
        {
            ratio = 1;
        }
        onde.SetFloat("_Effect_Opacity", ratio);
        cadrillage.SetFloat("_Scan_Effect_Opacity", ratio);
        highlight.SetFloat("_Highlight_Opacity", ratio);
        volume.weight = ratio;

    }

}
