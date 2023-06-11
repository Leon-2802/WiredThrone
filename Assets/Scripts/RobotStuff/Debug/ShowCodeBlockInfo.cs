using UnityEngine;
using UnityEngine.EventSystems;

public class ShowCodeBlockInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject blockInfo;
    void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        blockInfo.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        blockInfo.SetActive(false);
    }
}
