using Assets.UnityFoundation.EditorInspector;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.UnityFoundation.Systems.CarSystem
{
    public class CarInputs : MonoBehaviour
    {
        [Header("Car input values")]

        [SerializeField] [ShowOnly] private Vector2 move;
        [SerializeField] [ShowOnly] private Vector2 turn;

        public float Move => move.y;
        public float Turn => turn.x;

        private CarInputActions inputActions;

        private void Awake()
        {
            inputActions = new CarInputActions();
            inputActions.Car.Move.performed += OnMove;
            inputActions.Car.Move.canceled += OnMoveCanceled;

            inputActions.Car.Turn.performed += OnTurn;
            inputActions.Car.Turn.canceled += OnTurn;

            inputActions.Enable();
        }

        private void OnTurn(CallbackContext ctx) => turn = ctx.ReadValue<Vector2>();

        public void OnMove(CallbackContext ctx) => move = ctx.ReadValue<Vector2>();

        private void OnMoveCanceled(CallbackContext ctx) => move = Vector2.zero;

    }
}