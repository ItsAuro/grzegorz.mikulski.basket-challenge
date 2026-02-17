public class GameState
{
    private int _score         = 0;
    private int _remainingTime = GameConfig.MAX_TIME;
    private int _fireballValue = 0;

    public event System.Action<int> OnScoreChange;
    public event System.Action<int> OnRemainingTimeChange;
    public event System.Action      OnTimeEnd;
    public event System.Action<int> OnFireballValueChange;
    public event System.Action      OnFireballEnable;
    public event System.Action      OnFireballDisable;

    // time editors
    public void AddTime(int increment)
    {
        _remainingTime += increment;
        OnRemainingTimeChange?.Invoke(_remainingTime);
    }
    public void RemoveTime(int decrement)
    {
        _remainingTime -= decrement;

        if (_remainingTime <= 0)
        {
            _remainingTime = 0;
            OnTimeEnd?.Invoke();
        }
        OnRemainingTimeChange?.Invoke(_remainingTime);
    }
    // fireball editors
    public void AddFireball(int increment)
    {
        _fireballValue += increment;

        if (_fireballValue >= GameConfig.FIREBALL_THRESHOLD)
        {
            _fireballValue = GameConfig.FIREBALL_THRESHOLD;
            Fireball(true);
        }
        OnFireballValueChange?.Invoke(_fireballValue);
    }
    public void ResetFireball()
    {
        _fireballValue = 0;
        OnFireballValueChange?.Invoke(_fireballValue);
        Fireball(false);
    }
    private void Fireball(bool enable)
    {
        if(enable) OnFireballEnable?.Invoke();
        else OnFireballDisable?.Invoke();
    }
    // score editors
    public void AddScore(int score)
    { 
        _score += score;
        OnScoreChange?.Invoke(_score);
    }
    public void ResetScore()
    {
        _score = 0;
        OnScoreChange?.Invoke(_score);
    }
}
