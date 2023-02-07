using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UiProgrammerTest.Scripts.UI.Counters
{
    /// <summary>
    /// Отображает числовое значение
    /// </summary>
    public abstract class UiCounter : MonoBehaviour
    {
        protected abstract int Value { get; }

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField, Min(1)] private float _animationDuration = 1.2f;
        private int _targetValue;
        private int _currentValue;
        [SerializeField] private UnityEvent OnStartSetValueFx;



        protected virtual void Start()
        {
            // set value immidiatly
            _targetValue = _currentValue = Value;
            _text.text = Value.ToString();
        }

        private void OnEnable()
        {
            StartCoroutine(CountCoroutine());
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