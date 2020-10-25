using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public class Healthbar : MonoBehaviour
    {

        public Slider HealthSlider;
        public Color High;
        public Color Low;
        public Vector3 Offset;

        public void SetHealth(float health, float maxHealth)
        {
            HealthSlider.gameObject.SetActive(health < maxHealth);
            HealthSlider.value = health;
            HealthSlider.maxValue = maxHealth;

            var fill = (HealthSlider as UnityEngine.UI.Slider).GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
            if (fill != null)
            {
                fill.color = Color.Lerp(Color.red, Color.green, HealthSlider.normalizedValue);
            }
        }

        void Update()
        {
            HealthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
        }
    }
}