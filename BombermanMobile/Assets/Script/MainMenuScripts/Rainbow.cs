using UnityEngine;
using UnityEngine.UI;

public class Rainbow : MonoBehaviour
{
    private Image _bgImage;
    private float  _h;
    [SerializeField] private float _speed = 1;

    private void Awake()
    {
        _bgImage = GetComponent<Image>();
    }
    private void Update()
    {
        if (_bgImage)
        {
            if(_h < 1)
            {
                _h += Time.deltaTime * _speed/100;
                _bgImage.color = Color.HSVToRGB(_h, .47f, 1);
            }
            else
            {
                _h = 0;

                _bgImage.color = Color.HSVToRGB(_h, 47, 100);
            }
        }
        else
        {
            Debug.LogError("bgImage is empty");
        }
    }
}
