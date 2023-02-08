using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UiProgrammerTest.Scripts.UI.Counters
{
    /// <summary>
    /// UI анимированный счетчик целочисленных значений
    /// </summary>
    public abstract class UiCounter : MonoBehaviour
    {
        /// <summary>
        /// »сточник целевого значени€
        /// </summary>
        protected abstract int Value { get; }

        [SerializeField] private TextMeshProUGUI _text;
        [Tooltip("¬рем€, за которое текущее значение достигнет целевого")]
        [SerializeField, Min(1)] private float _animationDuration = 1.2f;
        private int _targetValue;
        private int _currentValue;
        [SerializeField] private UnityEvent OnStartSetValueFx;



        protected virtual void Start()
        {
            SetValueImmidiatly();
        }

        private void OnEnable()
        {
            SetValueImmidiatly();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }



        protected void SetValue(GameModel.OperationResult operationResult)
        {
            if (!operationResult.IsSuccess) return;

            SetValueInternal();
        }

        private void SetValueInternal()
        {
            _targetValue = Value;

            if (!isActiveAndEnabled) return;

            StopAllCoroutines();
            StartCoroutine(CountCoroutine());

            OnStartSetValueFx?.Invoke();
        }

        private void SetValueImmidiatly()
        {
            _targetValue = _currentValue = Value;
            _text.text = Value.ToString();
        }

        private IEnumerator CountCoroutine()
        {
            if (_currentValue == _targetValue) yield break;

            int framesCount = Mathf.RoundToInt(60 * _animationDuration);
            for (int i = 0; i <= framesCount; i++)
            {
                _currentValue = Mathf.RoundToInt(Mathf.Lerp(_currentValue, _targetValue, (float)i / framesCount));
                _text.text = _currentValue.ToString();
                yield return null;
            }
        }
    }
}