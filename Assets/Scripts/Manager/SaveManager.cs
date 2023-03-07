using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    #region Variables
    public static SaveManager instanceSaveManager { get; set; }
    public static float[] allAudioLevels;

    #endregion

    #region Built In methods

    // Start is called before the first frame update
    void Awake()
    {
        instanceSaveManager = this;
        
        allAudioLevels = new float[] { .8f, .8f, .8f, .8f, .8f, .8f, .8f, .8f, .8f, .8f }; //Je déclare 10 valeurs vitef
    }

    #endregion

    #region Customs Function

    /// <summary>
    /// Save the audio level
    /// </summary>
    /// <param name="levelAudio"> The number of the groupe audio</param>
    /// <param name="value"></param>
    public void SaveAudioLevel(int levelAudio, float value)
    {
        if (!AudioManager.AudioInstance) return;

        allAudioLevels[levelAudio] = value;
    }

    #endregion
}
