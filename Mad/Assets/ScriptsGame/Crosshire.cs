using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Crosshire : MonoBehaviour
{
    private Rect _canvasRect;
    private RectTransform _rectTr;

    private void Start()
    {
        _rectTr = GetComponent<RectTransform>();
        _canvasRect = _rectTr.parent.GetComponent<RectTransform>().rect;

    }

    private void Update()
    {
        // узнаём относительное положение изображения к канвасу (числа от 0 до 1)
        var x = _rectTr.anchoredPosition.x / _canvasRect.width;
        var y = _rectTr.anchoredPosition.y / _canvasRect.height;
        Vector2 viewportPos = new Vector2(x, y);
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hi3t = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hi3t.collider != null) { Debug.Log("Что-то было нажато!"); }
            Debug.Log($"{x} {y} {mousePos2D} {mousePos}");// пускаем луч из вьюпорта

            Ray ray = Camera.main.ViewportPointToRay(viewportPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.name);
            }

            var p = new Vector3(transform.position.x, transform.position.y, 0);
            RaycastHit2D ray2d = Physics2D.Raycast(new Vector3(p.x, p.y, 0), Vector2.zero);
            if (ray2d.collider != null)
            {
                IDamageble damageble;
                if (ray2d.collider.TryGetComponent<IDamageble>(out damageble))
                {
                    Debug.Log("R asdas DAMAGU: ");
                }
            }/**/
        }

        
        
        

    }
}
