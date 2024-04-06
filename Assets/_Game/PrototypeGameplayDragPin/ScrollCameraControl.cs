using System.Collections;
using UnityEngine;
using Utilities;

namespace PrototypeGameplayDragPin
{
    public class ScrollCameraControl : MonoBehaviour
    {
        [SerializeField] private GameplayControlDragPin _gameplayControlDragPin;
        [SerializeField] private DragTarget dragTarget;
        
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private AnimationCurve _mouseViewportPosXToHorizontalScrollSpeed = AnimationCurve.EaseInOut(0, -1, 1, 1);
        [SerializeField] private AnimationCurve _mouseViewportPosYToVerticalScrollSpeed = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private Rect _viewZone = new Rect(-10, 0, 20, 15);
        [SerializeField, Range(0f, 1f)] private float _lerpSpeedReturnGround = 0.1f;

        private bool _isDragMap;
        private Vector2 _prevWorldMousePos;
        
        

        private void LateUpdate()
        {
            if (dragTarget.IsDragging)
            {
                Vector2 viewportMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector2 scrollSpeed = new Vector2(
                    _mouseViewportPosXToHorizontalScrollSpeed.Evaluate(viewportMousePos.x),
                    _mouseViewportPosYToVerticalScrollSpeed.Evaluate(viewportMousePos.y));
                Vector2 cameraSize = Camera.main.ViewportToWorldPoint(Vector2.one) - Camera.main.ViewportToWorldPoint(Vector2.zero);
                Vector3 cameraPos = _cameraTransform.position;
                cameraPos.x = Mathf.Clamp(cameraPos.x + scrollSpeed.x * Time.deltaTime, _viewZone.xMin + cameraSize.x / 2f, _viewZone.xMax - cameraSize.x / 2f);

                cameraPos.y = Mathf.Clamp(cameraPos.y + scrollSpeed.y * Time.deltaTime, _viewZone.yMin + cameraSize.y / 2f, _viewZone.yMax - cameraSize.y / 2f);

                _cameraTransform.position = cameraPos;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _isDragMap = true;
                    _prevWorldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _isDragMap = false;
                }
                else if (Input.GetMouseButton(0))
                {

                }

                if (_isDragMap)
                {
                    Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 delta = _prevWorldMousePos - worldMousePos;

                    Vector2 cameraSize = Camera.main.ViewportToWorldPoint(Vector2.one) - Camera.main.ViewportToWorldPoint(Vector2.zero);
                    Vector3 cameraPos = _cameraTransform.position;
                    cameraPos.x = Mathf.Clamp(cameraPos.x + delta.x, _viewZone.xMin + cameraSize.x / 2f, _viewZone.xMax - cameraSize.x / 2f);
                    cameraPos.y = Mathf.Clamp(cameraPos.y + delta.y, _viewZone.yMin + cameraSize.y / 2f, _viewZone.yMax - cameraSize.y / 2f);
                    _cameraTransform.position = cameraPos;
                }
                else
                {
                    Vector2 cameraSize = Camera.main.ViewportToWorldPoint(Vector2.one) - Camera.main.ViewportToWorldPoint(Vector2.zero);
                    Vector3 cameraPos = _cameraTransform.position;
                    cameraPos.y = Mathf.LerpUnclamped(cameraPos.y, _viewZone.yMin + cameraSize.y / 2f, _lerpSpeedReturnGround * Time.deltaTime * 60);
                    _cameraTransform.position = cameraPos;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            GizmoUtility.DrawRect(_viewZone, Color.green);
        }
    }
}