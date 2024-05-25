using App.SimplesScipts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class NumberButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private readonly SEvent<int> _onClicked = new();
    private Button _thisButton;
    private int _number;

    public ISEvent<int> OnClicked => _onClicked;

    private void Awake()
    {

        bool isNumber = Int32.TryParse(_text.text, out _number);

        if (!isNumber)
            return;

        if (_number < 0 || _number > 9)
            return;

        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _onClicked.Invoke(_number);
    }

}
