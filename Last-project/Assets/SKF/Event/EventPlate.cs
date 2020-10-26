using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using SKFramework.EVENT;
namespace SKFramework.PLATE
{
    /// <summary>
    ///事件板块。
    /// </summary>
    public sealed class EventPlate : SKFrameworkPlateBase
    {
        
        private iSKFEventController mEventManager=null;
        private SKFEvent OnChangeHeightEvets = new SKFEvent();
       // public delegate void NEWEVENTS(object flags);
        
        protected override void Awake()
        {
            base.Awake();
            mEventManager = new EventInterface();
        }
        /// <summary>
        ///高度変化事件を予約
        /// </summary>
        public void SubscribeOnchangeHeight( UnityAction<object> e)
        {
            mEventManager.Subscribe(OnChangeHeightEvets, e);
        }
        /// <summary>
        ///高度変化事件をキャンセル
        /// </summary>
        public void　UnsubscribeOnchangeHeight(UnityAction<object> e)
        {
            mEventManager.Unsubscribe(OnChangeHeightEvets, e);
        }
        /// <summary>
        ///高度変化事件を打ち出す
        /// </summary>
        public void OnchangeHeightFire(object oe)
        {
            mEventManager.Fire(OnChangeHeightEvets, oe);
        }

        /// <summary>
        ///事件予約
        /// </summary>
        public void Subscribe(SKFEvent Event,  UnityAction<object> e)
        {
            mEventManager.Subscribe(Event, e);
        }
        /// <summary>
        ///事件キャンセル。
        /// </summary>
        public void Unsubscribe(SKFEvent Event, UnityAction<object> e)
        {
            mEventManager.Unsubscribe(Event, e);
        }
        /// <summary>
        ///全事件キャンセル。
        /// </summary>
        public void UnsubscribeAll(SKFEvent Event)
        {
            mEventManager.UnsubscribeAll(Event);
        }
        /// <summary>
        ///事件を起こす。
        /// </summary>
        public void Fire(SKFEvent Event, object oe)
        {
            mEventManager.Fire(Event,oe);
        }

    }
    
}
namespace SKFramework.EVENT
{
    /// <summary>
    ///事件管理。
    /// </summary>
    public interface iSKFEventController
    {
        void Subscribe(SKFEvent Event, UnityAction<object> e);
        void Unsubscribe(SKFEvent Event, UnityAction<object> e);
        void UnsubscribeAll(SKFEvent Event);
        void Fire(SKFEvent Event, object oe);

    }
    /// <summary>
    ///事件。
    /// </summary>
    public class SKFEvent : UnityEvent<object> { }
    public class EventInterface : iSKFEventController
    {
        public void Fire(SKFEvent Event, object oe)
        {
            Event.Invoke(oe);
        }

        public void Subscribe(SKFEvent Event, UnityAction<object> e)
        {
            Event.AddListener(e);

        }

        public void Unsubscribe(SKFEvent Event, UnityAction<object> e)
        {
            Event.RemoveListener(e);
        }

        public void UnsubscribeAll(SKFEvent Event)
        {
            Event.RemoveAllListeners();
        }
    }

}
