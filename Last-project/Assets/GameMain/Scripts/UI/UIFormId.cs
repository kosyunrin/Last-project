//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameName
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId : byte
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,

        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// 设置。
        /// </summary>
        SettingForm = 101,

        /// <summary>
        /// 关于。
        /// </summary>
        AboutForm = 102,
        /// <summary>
        /// GameStartProduce
        /// </summary>
        GameStartForm = 103,
        /// <summary>
        /// story
        /// </summary>
        AboutStorryForm = 104,
        /// <summary>
        /// 主要管理UI者
        /// </summary>
        AlllifeInMain = 105,
        /// <summary>
        /// 游戏大厅
        /// </summary>
        GameHallInMain = 106,
        /// <summary>
        /// 手术房间
        /// </summary>
        OpreateHallInMain = 107,
        /// <summary>
        /// 商店
        /// </summary>
        StoreHallInMain = 108,
        /// <summary>
        /// 设置
        /// </summary>
        SettingHallInMain = 109,
        /// <summary>
        /// 加载UI
        /// </summary>
        LoadingFrom = 110,
        /// <summary>
        /// Itempack
        /// </summary>
        ItemHouseForm = 111,
    }
}
