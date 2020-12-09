using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using SKFramework.GAMECONTROLLER;
using Invector.vCharacterController;
using SKFramework.EVENT;
using ENEMY_AI;

namespace SKFramework.PLATE
{
    /// <summary>
    ///ゲームコントローラーPlate
    /// </summary>
    public sealed partial class GameControllerPlate : SKFrameworkPlateBase
    {
        [SerializeField] AudioMixer Mixer=null;
        private iSKFGameController GameManager = null;
        private ObjectPoolsk<ObjectpoolBaseSK> mEnemyXBPool=null;

        public ObjectpoolBaseSK EnemyXiaoBing= null;
        public AudioMixer GetMixer { get { return Mixer; } }

        protected override void Awake()
        {
            base.Awake();
            GameManager = new GameControllerInterface(Mixer);
            mEnemyXBPool = new ObjectPoolsk<ObjectpoolBaseSK>();
        }
        private void Start()
        {
            mEnemyXBPool.Initialize(10, EnemyXiaoBing);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                Transform xiaobing0 = null;
                //Transform xiaobing1 = null;
                //Transform xiaobing2 = null;
                //Transform xiaobing3 = null;
                //Transform xiaobing4 = null;
                SKF.Game.ShowEnemyXiaoBing(ref xiaobing0);
                //SKF.Game.ShowEnemyXiaoBing(ref xiaobing1);
                //SKF.Game.ShowEnemyXiaoBing(ref xiaobing2);
                //SKF.Game.ShowEnemyXiaoBing(ref xiaobing3);
                //SKF.Game.ShowEnemyXiaoBing(ref xiaobing4);
                xiaobing0.position = new Vector3(-7, 5, 5.6f);
                SKMinorMonsterLogic minor0 = xiaobing0.GetComponent<SKMinorMonsterLogic>();
                minor0.RoadDictionary.Add(0, new Vector3(-7, 0, 5.6f));
                minor0.RoadDictionary.Add(1, new Vector3(-7, 0, 12));
                minor0.RoadDictionary.Add(2, new Vector3(-1, 0, 11));
                minor0.RoadDictionary.Add(3, new Vector3(-1, 0, 5));
                //xiaobing1.position = new Vector3(11, 5, 11);
                //xiaobing2.position = new Vector3(12, 5, -29);
                //xiaobing3.position = new Vector3(10, 5, -7);
                //xiaobing4.position = new Vector3(-6, 10, -7);
            }
        }

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
        /// <summary>
        ///取出小兵
        /// </summary>
        public void ShowEnemyXiaoBing()
        {
            mEnemyXBPool.TakeOut();
        }
        public void ShowEnemyXiaoBing(Vector3 WorldPos,Quaternion WolrdQuation)
        {
            mEnemyXBPool.TakeOut(WorldPos, WolrdQuation);
        }
        public void ShowEnemyXiaoBing(ref Transform OutObject)
        {
            mEnemyXBPool.TakeOut(ref OutObject);
        }
        /// <summary>
        ///收回小兵
        /// </summary>
        public void HiddenEnemyXiaoBing(ObjectpoolBaseSK x)
        {
            mEnemyXBPool.TakeBack(x);
        }




    }

    public sealed partial class GameControllerPlate
    {
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
