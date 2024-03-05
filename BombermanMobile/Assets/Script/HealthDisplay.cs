using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private AIUnit _unit;
    private Image _image;
    private Image _borderImage;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _borderImage = transform.parent.GetComponent<Image>();
        _image.enabled = false;
        _borderImage.enabled = _image.enabled;
        _image.fillAmount = GetHealthRatio();
        UpdateHealth();
    }

    void UpdateHealth()
    {
        if (_unit.Health < _unit.MaxHealth)
        {
            _image.enabled = true;
            _image.fillAmount = GetHealthRatio();
        }
        else
        {
            _image.enabled = false;
        }
        _borderImage.enabled = _image.enabled;
        Invoke("UpdateHealth", 0.1f);
    }
    private float GetHealthRatio()
    {
        return _unit.Health / _unit.MaxHealth;
    }
}
