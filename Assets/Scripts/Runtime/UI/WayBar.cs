using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class WayBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.value = 0;
    }

    public bool Evaluate(float fillDuration)
    {
        if (_slider.value < 1f)
        {
            _slider.value += Time.deltaTime / fillDuration;
            return false;
        }

        return true;
    }
}
