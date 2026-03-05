using System;

public interface IFishingMinigame
{
    event Action OnStart;
    event Action<bool> OnHandle;
    event Action OnFinish;
    event Action OnRestart;

    void Start();
    bool Handle(bool result); // View gọi vào
    void Restart();
}
