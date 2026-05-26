using UnityEngine;
using UnityEngine.InputSystem;

public class Player : characters
{
    protected bool _inRangeItem;
    private bool _inInventory;
    public Item _currentItem;
    // public float rangePlayer = 20f;
    [SerializeField] private Rigidbody _rb;
    private Vector3 _move;
    [SerializeField] private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput.enabled = false;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentHealth = _maxHealth; // Iniciamos con la vida maxima del player.
        Debug.Log("Se instancio el player en dicha posicion");
    }

    public override void OnNetworkSpawn()
    {
        _playerInput.enabled = IsOwner;
    }

    public override void OnNetworkDespawn() 
    {
        _playerInput.enabled = false;
    }

    //MOVIMIENTO
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

    //RECOLECCION

    private void OnPickUp(InputValue inputValue) // Metodo para la recoleccion del item.
    {
        Debug.Log("Valor del input: " + inputValue);
        if (inputValue.isPressed && _currentItem != null)// Si el inputValue esta siendo presionado y si "_currentIten" es verdadero.
        {
            _inInventory = true; // Actualizamos el valor de "_inInventory" a verdadero.
            Debug.Log("entra en la accion");
            _currentItem.collected(); //Llamamos al metodo "collected" del objeto con el que esta colisionando
        }
        else 
        {
            _inInventory = false; // Actualizamos el valor de "_inInventory" a falso.
            Debug.Log("No esta dejandose alzar");
        }
    }
    private void OnTriggerEnter(Collider other) //Metodo con el que verificamos la colision de entrada.
    {
        Item item = other.gameObject.GetComponent<Item>(); // Tomamos el componente "Item" del objeto colisionado, en caso de tenerlo.

        if (item != null) //Si "item" tiene un valor distinto de null.
        {
            _currentItem = item; //"_currentItem" va a ser igual a item.
            _inRangeItem = true; //"_inRangeItem" pasa a ser verdadero.

            Debug.Log("Estamos en rango para recoger");
        }


    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.gameObject.GetComponent<Item>();

        if (item != null) //Si "item" tiene un valor distinto de null.
        {
            _currentItem = null; //"_currentItem" va a ser igual a null.
            _inRangeItem = false; //"_inRangeItem" pasa a ser falso.

            Debug.Log("Estamos fuera de rango para recoger");
        }
    }

    public bool getItemSaved()//Metodo GET para enviar el valor de "_inInventory"
    {
        return _inInventory;
    }

    //TESTEO DE DAÑO
    private void OnTestDamage(InputValue value) // Usamos este metodo del nuevo imput system usar una key sin usar el if (Input.GetKeyDown(KeyCode.K)) del imput viejo que tira error.
    {
        if (value.isPressed) // presionamos la tecla configurada anteriormente en el ImputSystem_Actions.
        {
            TakeDamage(10); // Se usa para probar daño.
        }
    }

    private void Update()
    {
        //Debug.Log("El rango del objeto es: " + _inRangeItem);
        //Debug.Log("Esta en el inventario?: " + _inInventory);
        if (_inMove == true)//Se esta moviendo?
        {
            //Debug.Log("El cubo se esta moviendo por el mapa");
        }
        else
        {
            //Debug.Log("El cubo esta quieto");
        }
    }

}
