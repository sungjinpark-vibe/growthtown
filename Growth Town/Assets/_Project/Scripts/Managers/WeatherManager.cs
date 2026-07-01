using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public enum WeatherType { Clear, Rain, Snow }
    
    public WeatherType CurrentWeather { get; private set; }
    
    [SerializeField] private float weatherChangeInterval = 3600f; // 1 hour in seconds
    private float timer;

    private void Start()
    {
        ChangeWeather();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= weatherChangeInterval)
        {
            timer = 0f;
            ChangeWeather();
        }
    }

    private void ChangeWeather()
    {
        float rand = Random.value;
        if (rand < 0.1f) // 10% chance for Snow
        {
            CurrentWeather = WeatherType.Snow;
        }
        else if (rand < 0.3f) // 20% chance for Rain
        {
            CurrentWeather = WeatherType.Rain;
        }
        else // 70% chance for Clear
        {
            CurrentWeather = WeatherType.Clear;
        }
        
        Debug.Log($"Weather changed to: {CurrentWeather}");
        // Trigger particle systems or effects here based on CurrentWeather
    }
}
