using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using SKFramework.GAMECONTROLLER;
using UnityEngine.Events;

namespace SKFramework.PLATE
{
    /// <summary>
    ///ゲームコントローラーPlate
    /// </summary>
    public sealed class GameControllerPlate : SKFrameworkPlateBase
    {
        [SerializeField] AudioMixer Mixer=null;
        private iSKFGameController GameManager = null;
        protected override void Awake()
        {
            base.Awake();
            GameManager = new GameControllerInterface(Mixer);
        }
        public AudioMixer GetMixer { get { return Mixer; } }
        /// <summary>
        ///音を調整する
        ///0-1
        /// </summary>
        public void SetAllSoundValue(float value)
        {
            GameManager.SetSoundValue("MasterVolume", value);
        }
        /// <summary>
        ///音を調整する
        ///0-1
        /// </summary>
        public void SetMusicSoundValue(float value)
        {
            GameManager.SetSoundValue("MusicVolume", value);
        }
        /// <summary>
        ///音を調整する
        ///0-1
        /// </summary>
        public void SetSFXSoundValue(float value)
        {
            GameManager.SetSoundValue("SFXVolume", value);
        }
        /// <summary>
        ///ゲーム中止
        /// </summary>
        public void PauseGame()
        {
            GameManager.iPauseGame();
        }
        /// <summary>
        ///ゲーム回復
        /// </summary>
        public void RecoverGame()
        {
            GameManager.iRecoverGame();
        }
        /// <summary>
        ///ゲーム中止の間に特定の動画を回復する
        /// </summary>
        public void RecoverAnimaterSpeed(Animator anim)
        {
            GameManager.iRecoverAnimaterSpeed(anim);
        }
        /// <summary>
        ///通常ゲームの間における、特定の動画スビートを設定する。
        /// </summary>
        public void RevisionAnimaterSpeed(Animator anim, float f)
        {
            GameManager.iRevisionAnimaterSpeed(anim, f);
        }


        private void Start()
        {
           
        }
    }
}
namespace SKFramework.GAMECONTROLLER
{
    /// <summary>
    ///ゲーム管理者Interface
    /// </summary>
    public interface iSKFGameController
    {
        //sound
        float MaxAttenuation { get; }
        float MinAttenuation { get; }
        void SetSoundValue( string mixerParameter, float value);
        //time
        void iPauseGame();
        void iRecoverGame();
        void iRecoverAnimaterSpeed(Animator anim);
        void iRevisionAnimaterSpeed(Animator anim,float f);
        

    }

    public class GameControllerInterface : iSKFGameController
    {
        public GameControllerInterface(AudioMixer _Mixer)
        {
            Mixer = _Mixer;
        }
        public float MaxAttenuation => 0.0f;

        public float MinAttenuation => -80.0f;

        private AudioMixer Mixer = null;
        public void SetSoundValue(string mixerParameter, float value)
        {
            Mixer.SetFloat(mixerParameter, MinAttenuation + value * (MaxAttenuation - MinAttenuation));
        }

        public void iPauseGame()
        {
            Time.timeScale = 0;
        }

        public void iRecoverGame()
        {
            Time.timeScale = 1;
        }

        public void iRecoverAnimaterSpeed(Animator anim)
        {
            if(Time.timeScale==0)
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            else anim.updateMode = AnimatorUpdateMode.AnimatePhysics;
        }

        public void iRevisionAnimaterSpeed(Animator anim, float f)
        {
            anim.speed = f;
        }
    }

   

}
