using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaIndicator : MonoBehaviour
{
    [SerializeField] Wand _wand;

    TextMeshProUGUI _tmp;
    private void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _tmp.text = _wand.CurrentMana.ToString();
    }
}
