using System;
using System.Collections.Generic;
using UnityEngine;
using SKFramework.PLATE;

namespace SKFramework
{
    public partial class SKF : MonoBehaviour
    {

        [SerializeField]
        List<SKFrameworkPlateBase> PlateGroupsx = null;
        /// <summary>
        /// 获取事件模块。
        /// </summary>
        public static EventPlate Event
        {
            get;
            private set;
        }
        public static GameControllerPlate Game
        {
            get;
            private set;
        }
        private void OnEnable()
        {
            InintPlates();
        }


        private void InintPlates()
        {
            Event = GetPlate<EventPlate>();
            Game = GetPlate<GameControllerPlate>();
        }

        private SKFrameworkPlateBase GetPlate(Type type)
        {
            foreach (SKFrameworkPlateBase child in PlateGroupsx)
            {
                if (child.GetType() == type)
                {
                    return child;
                }
            }
            return null;
        }
        private T GetPlate<T>() where T : SKFrameworkPlateBase
        {
            return (T)GetPlate(typeof(T));
        }
    }
  
}

