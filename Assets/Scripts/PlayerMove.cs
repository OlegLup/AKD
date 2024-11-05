using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Transform cameraTransform;

    private InputAction moveAction;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector3 forwardProjection = Vector3.ProjectOnPlane(cameraTransform.forward, transform.up);
        transform.rotation = Quaternion.LookRotation(forwardProjection, Vector3.up);

        Vector3 move = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y);
        move = transform.rotation * move;
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }
}
