using UnityEngine;
using UnityEngine.UI;

/*
 자동차 트림 옵션과 토글 UI를 연결하는 스크립트입니다.
 */
public class TrimToggleConnector : MonoBehaviour
{
    public CarTrim trim;

    void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(HandleToggleChange);
    }

    void HandleToggleChange(bool isOn)
    {
        if (isOn)
        {
            CarConfiguratorManager.Instance.ApplyTrim(trim);
        }
    }
}