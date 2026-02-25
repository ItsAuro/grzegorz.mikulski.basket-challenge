using UnityEngine;

public class GameState : MonoBehaviour
{
    public int  Score          { private set; get; } = 0;
    public int  RemainingTime  { private set; get; } = GameConfig.MAX_TIME;
    public int  FireballValue  { private set; get; } = 0;
    public bool FireballStatus { private set; get; } = false;

    public event System.Action<int>   OnScoreChange;
    public event System.Action<int>   OnRemainingTimeChange;
    public event System.Action        OnTimeStart;
    public event System.Action        OnTimeEnd;
    public event System.Action<int>   OnFireballValueChange;
    public event System.Action        OnFireballEnable;
    public event System.Action        OnFireballDisable;

    // time editors
    public void AddTime(int increment)
    {
        RemainingTime += increment;
        OnRemainingTimeChange?.Invoke(RemainingTime);
    }
    public void RemoveTime(int decrement)
    {
        RemainingTime -= decrement;

        if (RemainingTime <= 0)
        {
            RemainingTime = 0;
            OnTimeEnd?.Invoke();
            CancelInvoke(nameof(TimeStep));
        }
        OnRemainingTimeChange?.Invoke(RemainingTime);
    }
    public void StartTimer()
    {
        OnTimeStart?.Invoke();
        InvokeRepeating(nameof(TimeStep), 1, 1);
    }
    private void TimeStep()
    {
        RemoveTime(1);
    }
    // fireball editors
    public void AddFireball(int increment)
    {
        FireballValue += increment;

        if (FireballValue >= GameConfig.FIREBALL_THRESHOLD)
        {
            FireballValue = GameConfig.FIREBALL_THRESHOLD;
            FireballStatus = true;
            OnFireballEnable?.Invoke();
        }
        OnFireballValueChange?.Invoke(FireballValue);
    }
    public void ResetFireball()
    {
        FireballValue = 0;
        FireballStatus = false;

        OnFireballValueChange?.Invoke(FireballValue);
        OnFireballDisable?.Invoke();
    }
    // score editors
    public void AddScore(int score)
    {
        Score += score;
        OnScoreChange?.Invoke(Score);
    }
    public void ResetScore()
    {
        Score = 0;
        OnScoreChange?.Invoke(Score);
    }

}
