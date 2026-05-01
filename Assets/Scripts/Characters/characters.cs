using UnityEngine;
using UnityEngine.InputSystem;

public class characters : MonoBehaviour
{
    private Rigidbody _rb;
    protected bool _inMove;
    private Vector3 _move;
    //Atributos del personaje
    private int _lives;
    protected int _speed = 4;
<<<<<<< HEAD

    //Atributo de vida 
    [SerializeField] private float _maxHealth = 100f; // Se declara la vida maxima y se hace visible en el inspector.
    protected float _currentHealth;                   // Permite controlar la vida del player.

    void Start()
=======
    void Awake()
>>>>>>> Player_Attack
    {
        _rb = GetComponent<Rigidbody>();

        _currentHealth = _maxHealth; // Iniciamos con la vida maxima del player.
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
    }

    public void TakeDamage(float amount) // Usamos este metodo en public para que pueda ser llamado y bajar vida.
    {
        _currentHealth -= amount; // Se resta el daño de la cantidad de vida actual.
        Debug.Log("Vida actual: " + _currentHealth); // Mostramos en consola cuánta vida queda.

        if (_currentHealth <= 0) // Cuando la vida menor o igual a cero morimos.
        {
            Die(); // morimos.
        }
<<<<<<< HEAD
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto"); // Avisa que se va a destruir el objeto player.
        Destroy(gameObject); // Eliminamos el objeto player.
    }
=======
        
       
       public virtual void TakeDamage(float amount) // Virtual sirve para que pueda ser modificado o rescrito por cualquiera de las clases hijas (enemy / player).
       {
        // Queda vacio o virtual para que cada hijo/a sea modificado
       }
>>>>>>> Player_Attack
}
