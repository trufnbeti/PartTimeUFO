using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace PrototypeGameplayDragPin
{
    public class GameplayControlDragPin : MonoBehaviour
    {
        private List<DragableRigidbody2DJoint> _dragableRigidbody2DJoint;
        public List<PinPoint> _pin = null;

        private DragableRigidbody2DJoint _draggingObject;
        private PinPoint _selectedPin;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
        }

        private void Start()
        {
            _dragableRigidbody2DJoint = new List<DragableRigidbody2DJoint>(FindObjectsOfType<DragableRigidbody2DJoint>());
            foreach (var dragableRigidbody2DJoint in _dragableRigidbody2DJoint)
            {
                dragableRigidbody2DJoint.OnStartDrag.AddListener(OnStartDragObject);
                dragableRigidbody2DJoint.OnEndDrag.AddListener(OnEndDragObject);
            }

            _pin = new List<PinPoint>(FindObjectsOfType<PinPoint>());
            _selectedPin = null;
        }

        private void OnStartDragObject(DragableRigidbody2DJoint dragObject)
        {
            if (_draggingObject)
            {
                return;
            }
            _draggingObject = dragObject;
        }

        private void Update()
        {
            if (_draggingObject)
            {
                Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                PinPoint selectedPin = null;

                foreach (var pin in _pin)
                {
                    if (pin.IsPointInsidePin(worldMousePos))
                    {
                        selectedPin = pin;
                        break;
                    }
                }

                if (selectedPin != _selectedPin)
                {
                    _selectedPin?.SetHighlight(false);
                    _selectedPin = selectedPin;
                    _selectedPin?.SetHighlight(true);
                }
            }
        }

        private void OnEndDragObject(DragableRigidbody2DJoint dragObject)
        {
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach (var pin in _pin)
            {
                if (pin.IsPointInsidePin(worldMousePos))
                {
                    pin.CreateSpringJoint(dragObject as DragableRigidbody2DSpringJoint);
                    break;
                }
            }

            if (_draggingObject == dragObject)
            {
                _draggingObject = null;
            }
        }
    }
}