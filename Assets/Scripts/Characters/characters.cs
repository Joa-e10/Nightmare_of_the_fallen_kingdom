using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.InputSystem;

public class characters : MonoBehaviour
{
    //Para el PickUp
    private bool _isCollectObject;
    private bool _CollectInRange;
    public float rangePlayer = 20f;
    private RaycastHit _hitCollect;
    public LayerMask hitLayerCollect;
    private Transform _rangeCheckPlayer;


    private Rigidbody _rb;
    protected bool _inMove;
    private Vector3 _move;
    //Atributos del personaje
    private int _lives;
    protected int _speed = 4;

    //Atributo de vida 
    [SerializeField] private float _maxHealth = 100f; // Se declara la vida maxima y se hace visible en el inspector.
    protected float _currentHealth;                   // Permite controlar la vida del player.

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rangeCheckPlayer = GameObject.Find("RangePlayer").GetComponent<Transform>();

        _currentHealth = _maxHealth; // Iniciamos con la vida maxima del player.
    }

    private void OnPickUp(InputValue inputValue) 
    {
        if (inputValue.isPressed && _CollectInRange == true)
        {
            _isCollectObject = true;
        }
        else 
        {
            _isCollectObject = false;
        }
    }

    public bool getIsCollectObject() 
    {
        return _isCollectObject;
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

    
    private void OnTestDamage(InputValue value) // Usamos este metodo del nuevo imput system usar una key sin usar el if (Input.GetKeyDown(KeyCode.K)) del imput viejo que tira error.
    {
        if (value.isPressed) // presionamos la tecla configurada anteriormente en el ImputSystem_Actions.
        {
            TakeDamage(10f); // Se usa para probar daño.
        }
    }

    void Update()
    {
        //Debug.Log("El input value tira: " + _move);
        if (_inMove == true)//Se esta moviendo?
        {
            //Debug.Log("El cubo se esta moviendo por el mapa");
        }
        else
        {
            //Debug.Log("El cubo esta quieto");
        }

        if (Physics.Raycast(_rangeCheckPlayer.position, transform.forward, out _hitCollect, rangePlayer, hitLayerCollect)) //Creamos una deteccion por rayo y consultamos si colisiono con un objeto "Player".
        {
            Debug.Log("Colisiono con un objeto! " + _hitCollect.collider.gameObject.name);
            _CollectInRange = true;

        }
        else
        {
            Debug.Log("No esta colisionando con un recolectable! ");
            _CollectInRange = false;
        }
            Debug.DrawLine(_rangeCheckPlayer.position, _hitCollect.point * rangePlayer, Color.green); //Dibuja en la escena el rayo de deteccion.
    }

    public void TakeDamage(float amount) // Usamos este metodo en public para que pueda ser llamado y bajar vida.
    {
        _currentHealth -= amount; // Se resta el daño de la cantidad de vida actual.
        Debug.Log("Vida actual: " + _currentHealth); // Mostramos en consola cuánta vida queda.

        if (_currentHealth <= 0) // Cuando la vida menor o igual a cero morimos.
        {
            Die(); // morimos.
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto"); // Avisa que se va a destruir el objeto player.
        Destroy(gameObject); // Eliminamos el objeto player.
    }
}
