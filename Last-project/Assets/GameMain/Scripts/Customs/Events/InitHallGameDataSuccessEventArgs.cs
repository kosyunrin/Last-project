using GameFramework;
using GameFramework.Event;
/// <summary>
/// 加载资源成功事。
/// </summary>
public sealed class LoadNextResourcesSuccessArgs : GameEventArgs
{
    public static readonly int EventId = typeof(LoadNextResourcesSuccessArgs).GetHashCode();


    
    public bool isInitSuccess { get; private set; }
    public LoadNextResourcesSuccessArgs Fill(bool isInitSuccessargs)
    {
        isInitSuccess = isInitSuccessargs;
        return this;
    }

   

    public override void Clear()
    {
        isInitSuccess = false;
    }

    /// <summary>
    /// 获取成功事件编号。
    /// </summary>
    public override int Id => EventId;
}
