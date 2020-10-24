//using System;
//namespace SKFramework.Static
//{
//    public static class SKF
//    {
//        public static T GetPlate<T>() where T : SKFrameworkPlateBase
//        {
//            return (T)GetPlate(typeof(T));
//        }

//        /// <summary>
//        /// 获取游戏框架组件。
//        /// </summary>
//        /// <param name="type">要获取的游戏框架组件类型。</param>
//        /// <returns>要获取的游戏框架组件。</returns>
//        public static SKFrameworkPlateBase GetPlate(Type type)
//        {
//            foreach (SKFrameworkPlateBase child in SKFramework.S)
//            {
//                if (child.GetType() == type)
//                {
//                    return child;
//                }
//            }

//            return null;
//        }
//    }
//}