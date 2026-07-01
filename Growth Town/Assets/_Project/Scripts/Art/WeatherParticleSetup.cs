using UnityEngine;

namespace LifeTown.Art
{
    [RequireComponent(typeof(ParticleSystem))]
    public class WeatherParticleSetup : MonoBehaviour
    {
        public enum WeatherType
        {
            Rain,
            Snow
        }

        public WeatherType weatherType;

        [ContextMenu("Apply URP Settings")]
        public void ApplySettings()
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            var main = ps.main;
            var emission = ps.emission;
            var shape = ps.shape;
            var renderer = ps.GetComponent<ParticleSystemRenderer>();

            if (weatherType == WeatherType.Rain)
            {
                main.startSpeed = 15f;
                main.startSize = 0.1f;
                main.startColor = new Color(0.8f, 0.9f, 1f, 0.6f);
                main.gravityModifier = 1.5f;

                emission.rateOverTime = 500f;

                shape.shapeType = ParticleSystemShapeType.Box;
                shape.scale = new Vector3(20f, 1f, 20f);

                renderer.renderMode = ParticleSystemRenderMode.Stretch;
                renderer.lengthScale = 2f;
            }
            else if (weatherType == WeatherType.Snow)
            {
                main.startSpeed = 2f;
                main.startSize = 0.2f;
                main.startColor = new Color(1f, 1f, 1f, 0.8f);
                main.gravityModifier = 0.1f;

                emission.rateOverTime = 300f;

                shape.shapeType = ParticleSystemShapeType.Box;
                shape.scale = new Vector3(20f, 1f, 20f);

                var noise = ps.noise;
                noise.enabled = true;
                noise.strength = 0.5f;
                noise.frequency = 0.5f;

                renderer.renderMode = ParticleSystemRenderMode.Billboard;
            }
            
            Debug.Log($"Applied {weatherType} settings to Particle System.");
        }
    }
}
