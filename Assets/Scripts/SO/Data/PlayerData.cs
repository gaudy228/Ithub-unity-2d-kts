using System.ComponentModel;
using Unity.Properties;

public class PlayerData : INotifyPropertyChanged
{
    private int _score;

    [CreateProperty]
    public int Score
    {
        get => _score;
        set
        {
            if (_score != value)
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void AddScore(int value)
    {
        Score += value;
    }
}