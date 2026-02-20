public class GameState
{
    public int  Score          { private set; get; } = 0;
    public int  RemainingTime  { private set; get; } = GameConfig.MAX_TIME;
    public int  FireballValue  { private set; get; } = 0;
    public bool FireballStatus { private set; get; } = false;

    public event System.Action<int> OnScoreChange;
    public event System.Action<int> OnRemainingTimeChange;
    public event System.Action      OnTimeEnd;
    public event System.Action<int> OnFireballValueChange;
    public event System.Action      OnFireballEnable;
    public event System.Action      OnFireballDisable;

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
        }
        OnRemainingTimeChange?.Invoke(RemainingTime);
    }
    // fireball editors
    public void AddFireball(int increment)
    {
        FireballValue += increment;

        if (FireballValue >= GameConfig.FIREBALL_THRESHOLD)
        {
            FireballValue = GameConfig.FIREBALL_THRESHOLD;
            Fireball(true);
        }
        OnFireballValueChange?.Invoke(FireballValue);
    }
    public void ResetFireball()
    {
        FireballValue = 0;
        OnFireballValueChange?.Invoke(FireballValue);
        Fireball(false);
    }
    private void Fireball(bool enable)
    {
        if(enable) 
        {
            FireballStatus = true;
            OnFireballEnable?.Invoke(); 
        }
        else 
        { 
            FireballStatus = false;
            OnFireballDisable?.Invoke(); 
        }
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
