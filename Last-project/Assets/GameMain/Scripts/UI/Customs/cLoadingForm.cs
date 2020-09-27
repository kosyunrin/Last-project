using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

namespace GameName
{
    public class cLoadingForm : UGuiForm
    {
        [Header("Settings")]
        [Range(0.5f, 7)] public float SceneSmoothLoad = 3;
        [Range(0.5f, 7)] public float FadeOutSpeed = 2;
        [Range(50, 1000)] public float LoadingCircleSpeed = 300;
        [Range(0.1f, 1.0f)] public float CloseDelay = 0.1f;

        public bool useTimeScale = false;
        [Header("Background")]
        public bool useBackgrounds = true;
        [Range(1, 60)] public float TimePerBackground = 5;
        [Range(0.5f, 7)] public float FadeBackgroundSpeed = 2;
        [Range(0.5f, 5)] public float TimeBetweenBackground = 0.5f;

        [Header("References")]
        [SerializeField] private Text SceneNameText = null;
        [SerializeField] private Text DescriptionText = null;
        [SerializeField] private Text ProgressText = null;
        [SerializeField] private Image BackgroundImage = null;
        [SerializeField] private Image FilledImage = null;
        [SerializeField] private GameObject RootUI;
        [SerializeField] private GameObject FlashImage = null;
        [SerializeField] private CanvasGroup FadeImageCanvas = null;
        [SerializeField] private RectTransform LoadingCircle = null;
        [SerializeField] private CanvasGroup LoadingCircleCanvas = null;
        [SerializeField]
        private List<Sprite> cacheBackgrounds = null;

        private CanvasGroup BackgroundAlpha = null;
        private int CurrentBackground = 0;
        private float lerpValue = 0;
        //private ProcedureChangeScene m_p = null;
        private int CountDown = 0;
        private bool Finish = false;
        public int NextSceneMusicID { set; get; }
        public UnityGameFramework.Runtime.Entity mPlayerManagaer { get; set; }



        void UpdateUI()
        {
            {
               // float Extra = 0.1f;
                //float p = (async.progress + Extra); //Fix problem of 90%
                lerpValue = Mathf.Lerp(lerpValue, 1.0f, DeltaTime * SceneSmoothLoad);
                if (FilledImage != null) { FilledImage.fillAmount = lerpValue; }
                if (lerpValue > 0.99f)
                {
                    if (!Finish)
                    {
                        //OnFinish();
                    }
                }

                //if (ProgressText != null)
                //{
                //    string percent = (lerpValue * 100).ToString("F0");
                //    ProgressText.text = string.Format(LoadingTextFormat, percent);
                //}
            }
        }
        void UpdateUI2(float dt)
        {
            //if(m_p!=null)
            lerpValue = Mathf.Lerp(lerpValue, CountDown, dt * SceneSmoothLoad);
            if (CountDown > 0)  FilledImage.fillAmount = lerpValue;
            else LoadingRotator();
            if (lerpValue>0.99f)
            {
                if (!Finish)
                {
                    OnFinish();
                }
            }
            if(LoadingCircleCanvas.alpha<=0)
            {
                Close(true);
            }
        }


        void OnFinish()
        {
            Finish = true;
            GameEntry.Sound.PlayUISound(10002);
            if (FlashImage != null) { FlashImage.SetActive(true); }
            if (LoadingCircleCanvas != null) { StartCoroutine(FadeOutCanvas(LoadingCircleCanvas)); }
        }



        public void OpenRangeBackGrounds()
        {
            if (BackgroundImage != null && useBackgrounds)
            {
                if (cacheBackgrounds.Count > 1)
                {
                    //cacheBackgrounds.AddRange(Backgrounds);
                    StartCoroutine(BackgroundTransition());
                    BackgroundImage.color = Color.white;
                }
                else if (cacheBackgrounds != null && cacheBackgrounds.Count > 0)
                {
                    BackgroundImage.sprite = cacheBackgrounds[0];
                    BackgroundImage.color = Color.white;
                }
            }

        }

        void LoadingRotator()
        {
            if (LoadingCircle == null)
                return;

            LoadingCircle.Rotate(-Vector3.forward * DeltaTime * LoadingCircleSpeed);
        }

        IEnumerator BackgroundTransition()
        {
            while (true)
            {
                BackgroundImage.sprite = cacheBackgrounds[CurrentBackground];
                while (BackgroundAlpha.alpha < 1)
                {
                    BackgroundAlpha.alpha += DeltaTime * FadeBackgroundSpeed * 0.8f;
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(TimePerBackground);
                while (BackgroundAlpha.alpha > 0)
                {
                    BackgroundAlpha.alpha -= DeltaTime * FadeBackgroundSpeed;
                    yield return new WaitForEndOfFrame();
                }
                CurrentBackground = (CurrentBackground + 1) % cacheBackgrounds.Count;
                yield return new WaitForSeconds(TimeBetweenBackground);
            }
        }

        private IEnumerator FadeOutCanvas(CanvasGroup alpha)
        {
            yield return new WaitForSeconds(CloseDelay);
            while (alpha.alpha > 0)
            {
                alpha.alpha -= DeltaTime * FadeOutSpeed;
                yield return null;
            }
        }




        private float DeltaTime
        {
            set;get;
        }


#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
            //m_p = (ProcedureChangeScene)userData;
            GameEntry.Event.Subscribe(LoadNextResourcesSuccessArgs.EventId, CloseThis);

            CountDown = 0;
            lerpValue = 0;
            Finish = false;
            if (FlashImage != null) { FlashImage.SetActive(false); }
        
            if (BackgroundImage != null) { BackgroundAlpha = BackgroundImage.GetComponent<CanvasGroup>(); }
            if (FilledImage != null) { FilledImage.type = Image.Type.Filled; FilledImage.fillAmount = 0.3f; }



        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {

            base.OnOpen(userData);
            GameEntry.Sound.PlayMusic(4);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            DeltaTime = elapseSeconds;
            UpdateUI2(elapseSeconds);

        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            GameEntry.Event.Unsubscribe(LoadNextResourcesSuccessArgs.EventId, CloseThis);
            GameEntry.Sound.PlayMusic(NextSceneMusicID);
            base.OnClose(isShutdown, userData);
            if(mPlayerManagaer)
            {
                Test mLogic = (Test)mPlayerManagaer.Logic;
                mLogic.Visible = true;
            }

        }


        void CloseThis(object sender, GameEventArgs e)
        {
            CountDown = 1;
            Debug.Log("quan bu duqu");
        }
    
    }

}
