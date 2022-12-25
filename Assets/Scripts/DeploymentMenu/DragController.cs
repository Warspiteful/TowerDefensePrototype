using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeploymentMenu{
public class DragController : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler
    {
    
        private VoidCallback _StartDragVoidCallback;
        private VoidCallback _stateChangeCallback;

        private VoidCallback _EndDragVoidCallback;

        private bool _isEnabled;


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isEnabled)
            {
                _StartDragVoidCallback?.Invoke();
                _stateChangeCallback?.Invoke();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            return;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(_isEnabled)
            {
            _EndDragVoidCallback?.Invoke();
            }
        }

    

        public void RegisterStartDragHandler(VoidCallback _callback)
        {
            _StartDragVoidCallback += _callback;
        }

        public void RegisterStateChange(VoidCallback _callback)
        {
            _stateChangeCallback += _callback;
        }
        public void ClearStateChange()
        {
            _stateChangeCallback = delegate {  };
        }

        public void Toggle(bool value)
        {
            _isEnabled = value;
        }


        public void RegisterEndDragHandler(VoidCallback _callback)
        {
            _EndDragVoidCallback += _callback;
        }


    }
}
