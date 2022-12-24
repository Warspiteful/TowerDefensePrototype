using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeploymentMenu{
public class DragController : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler
    {
    
        private VoidCallback _StartDragVoidCallback;
        private VoidCallback _EndDragVoidCallback;


        public void OnBeginDrag(PointerEventData eventData)
        {
            _StartDragVoidCallback?.Invoke();
    
        }

        public void OnDrag(PointerEventData eventData)
        {
            return;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _EndDragVoidCallback?.Invoke();
        }

        public void RegisterStartDragHandler(VoidCallback _callback)
        {
            _StartDragVoidCallback += _callback;
        }


        public void RegisterEndDragHandler(VoidCallback _callback)
        {
            _EndDragVoidCallback += _callback;
        }


    }
}
