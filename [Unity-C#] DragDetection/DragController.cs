using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts
{
    public enum DragState : int
    {
        None = 0,
        Dragging = 1,
        Dragged = 2,
        Started = 3,
        Ended = 4
    }

    public enum DragDirection : int
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
    public class DragController : MonoBehaviour
    {
        public static Action<DragEventModel> OnDragStart;
        public static Action<DragEventModel> OnDraggin;
        public static Action<DragEventModel> OnDragEnd;

        private DragEventModel model;

        [SerializeField] private bool EmitStart = true;
        [SerializeField] private bool EmitProgress = false;
        [SerializeField] private bool EmitEnd = true;

        [SerializeField] private bool GetFirstTarget = true;
        [SerializeField] private bool GetAllTargets = false;



        private bool started => Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
        private bool isDragging => Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved);
        private bool ended => Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended);

        private void Update()
        {
            if (started)
                StartDrag();
            if (isDragging)
                UpdateDrag();
            if (ended)
                EndDrag();
        }

        private void StartDrag()
        {
            model = new DragEventModel();
            model.State = DragState.Started;
            model.StartPosition = GetMousePosition();
            model.CurrentPosition = model.StartPosition;
            model.Target = GetTarget();

            if (EmitStart)
                OnDragStart?.Invoke(model);
        }

        private void UpdateDrag()
        {
            model.State = DragState.Dragging;
            model.CurrentPosition = GetMousePosition();
            model.VerticalDirection = GetVerticalDirection();
            model.HorizontalDirection = GetHorizontalDirection();
            model.DraggedDistance = Vector2.Distance(model.StartPosition, model.CurrentPosition);
            AddTargetsThroughTheWay();
            if (EmitProgress)
                OnDraggin?.Invoke(model);
        }

        private void EndDrag()
        {
            model.State = DragState.Ended;
            model.CurrentPosition = GetMousePosition();
            model.EndPosition = GetMousePosition();
            model.VerticalDirection = GetVerticalDirection();
            model.HorizontalDirection = GetHorizontalDirection();
            model.DraggedDistance = Vector2.Distance(model.StartPosition, model.CurrentPosition);
            if (EmitEnd)
                OnDragEnd?.Invoke(model);
        }

        private GameObject GetTarget()
        {
            if (!GetFirstTarget)
                return null;
            var ray = Camera.main.ScreenPointToRay(GetMousePosition());
            if (Physics.Raycast(ray, out var hit))
                return hit.collider.gameObject;
            return null;
        }

        private void AddTargetsThroughTheWay()
        {
            if (!GetAllTargets)
                return;
            var ray = Camera.main.ScreenPointToRay(GetMousePosition());
            if (Physics.Raycast(ray, out var hit))
            {
                if (!model.Targets.Contains(hit.collider.gameObject))
                    model.Targets.Add(hit.collider.gameObject);
            }

        }

        private DragDirection GetHorizontalDirection()
        {
            if (model.CurrentPosition.x > model.StartPosition.x)
                return DragDirection.Right;
            if (model.CurrentPosition.x < model.StartPosition.x)
                return DragDirection.Left;
            return DragDirection.None;
        }

        private DragDirection GetVerticalDirection()
        {
            if (model.CurrentPosition.y > model.StartPosition.y)
                return DragDirection.Up;
            if (model.CurrentPosition.y < model.StartPosition.y)
                return DragDirection.Down;
            return DragDirection.None;
        }

        private Vector2 GetMousePosition()
        {
            if (Input.touchCount > 0)
                return Input.GetTouch(0).position;
            else
                return Input.mousePosition;
        }
    }


    public class DragEventModel
    {
        public DragState State { get; set; }
        public DragDirection VerticalDirection { get; set; }
        public DragDirection HorizontalDirection { get; set; }
        public Vector2 StartPosition { get; set; }
        public Vector2 CurrentPosition { get; set; }
        public Vector2 EndPosition { get; set; }
        public float DraggedDistance { get; set; }
        public GameObject Target { get; set; }
        public IList<GameObject> Targets { get; set; }

        public DragEventModel()
        {
            Targets = new List<GameObject>();
        }
    }
}