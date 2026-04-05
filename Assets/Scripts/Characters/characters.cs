using UnityEngine;
using UnityEngine.InputSystem;

public class characters : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _inMove;
    private Vector3 _move;
    //Atributos del personaje
    private int _lives;
    private int _speed = 4;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue inputValue)  // Utilizamos el metodo OnMove designado para la accion de mover.
    {

      _move = new Vector3(inputValue.Get<Vector2>().x, 0, inputValue.Get<Vector2>().y); // Guardamos el valor del "InputValue" en un Vector3 para poder cambiar el valor entrante del eje y al z. 
        _rb.linearVelocity = _move * _speed; // generamos el movimiento del cubo.

        if (_move.x > 0 || _move.z > 0) // El eje x o y son mayores a 0?
        {
            _inMove = true;
        }
        else
        {
            _inMove = false;
        }
    }
        void Update()
        {
          Debug.Log("El input value tira: " + _move);
        if (_inMove == true)//Se esta moviendo?
        {
            Debug.Log("El cubo se esta moviendo por el mapa");
        }
        else 
        {
            Debug.Log("El cubo esta quieto");
        }
        
        }
}
