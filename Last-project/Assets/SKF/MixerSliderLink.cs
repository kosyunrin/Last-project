using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SKFramework;
namespace Gamekit3D
{
    [RequireComponent(typeof(Slider))]
    public class MixerSliderLink : MonoBehaviour
    {

        protected Slider m_Slider;


        void Awake ()
        {
            m_Slider = GetComponent<Slider>();

            //float value;
            //SKF.Game.GetMixer.GetFloat("SFXVolume", out value);


            //m_Slider.onValueChanged.AddListener(SliderValueChange);
        }
        private void Start()
        {
            //SKF.Game.SetSFXSoundValue(1);

        }
        private void Update()
        {
        }

       
    }
}