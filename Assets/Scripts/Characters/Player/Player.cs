using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : characters
{
    //ATRIBUTOS
    private Dictionary<string, int> _attributes = new Dictionary<string, int>()
    {
        {"Attack", 0},
        {"Mana", 0},
        {"Defense", 0}
    };

    private Inventory _hud;
    private bool _inRange;
    public ItemData _currentItemData;
    public Item _currentItem;
    // public float rangePlayer = 20f;
    private Vector3 _move;
    [SerializeField] private PlayerInput _playerInput;
    public Transform _inventoryLimit;
    public Transform _invLimitT;
    public Canvas _canvasManager;
    public Image _backgroundInventory;
    private bool _usingItem;

    //WEAPONS
    private bool attacking;
    [SerializeField]private GameObject _weaponMelee;

    //Character controller

    private CharacterController _characterController;
    private Vector2 _input;
    private float _yVelocity;
    private float _gravity = -9.81f;

    private void Awake()
    {
        
    }

    void Start()
    {
        _hud = GetComponent<Inventory>();
        _characterController = GetComponent<CharacterController>();
        //_currentHealth = _maxHealth; // Iniciamos con la vida maxima del player.
        _currentHealth = 100;
    }

    public override void OnNetworkSpawn()
    {
        _backgroundInventory = GameObject.Find("BackgroundInventory").GetComponent<Image>();
        _inventoryLimit = GameObject.Find("InventoryLimit").GetComponent<Transform>();
        _invLimitT = GameObject.Find("InventoryLimit").GetComponent<Transform>();
        _playerInput.enabled = IsOwner;
    }

    public override void OnNetworkDespawn() 
    {
        _playerInput.enabled = false;
    }

    private Vector3 GetCameraRelativeDirection()
    {
        Transform cam = Camera.main.transform;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        return camRight * _input.x + camForward * _input.y;
    }

    private Vector3 ApplyGravity(Vector3 moveDirection)
    {
        if (_characterController.isGrounded && _yVelocity < 0)
        {
            _yVelocity = -2f;
        }

        _yVelocity += _gravity * Time.deltaTime;
        moveDirection.y = _yVelocity;

        return moveDirection;
    }

    private void RotateCharacter(Vector3 moveDirection)
    {
        if (moveDirection.magnitude <= 0.1f) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    private void MoveCharacter(Vector3 moveDirection)
    {
        _characterController.Move(moveDirection * _speed * Time.deltaTime);
    }

    //MOVIMIENTO
    private void OnMove(InputValue inputValue)  // Utilizamos el metodo OnMove designado para la accion de mover.
    {
        _input = inputValue.Get<Vector2>();
        attacking = true;
    }

    //RECOLECCION
    private void OnPickUp(InputValue inputValue) // Metodo para la recoleccion del item.
    {
        Debug.Log("Valor del input: " + inputValue);
        if (inputValue.isPressed && _currentItem != null)// Si el inputValue esta siendo presionado y si "_currentIten" es verdadero.
        {
            _hud.AddItem(_currentItemData, _currentItem.getItemQuantity());
             pickUpServerRpc();
       
        }
        else 
        {
            _inRange = false; // Actualizamos el valor de "_inInventory" a falso.
            Debug.Log("No esta dejandose alzar");
        }

        
    }
    [ServerRpc]
    private void pickUpServerRpc()
    { 
        _inRange = true; // Actualizamos el valor de "_inInventory" a verdadero.
        Debug.Log("entra en la accion");
        _currentItem.collected(); //Llamamos al metodo "collected" del objeto con el que esta colisionando
    }

    private void OnTriggerEnter(Collider other) //Metodo con el que verificamos la colision de entrada.
    {
        Item item = other.gameObject.GetComponent<Item>(); // Tomamos el componente "Item" del objeto colisionado, en caso de tenerlo.

        if (item != null) //Si "item" tiene un valor distinto de null.
        {
            _currentItem = item; //"_currentItem" va a ser igual a item.
            _currentItemData = _currentItem._itemData;
            _currentItem._player = this.GetComponent<Player>();
            _currentItem._playerInventory = this.GetComponent<Inventory>();
        }


    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.gameObject.GetComponent<Item>();

        if (item != null) //Si "item" tiene un valor distinto de null.
        {
            item._player = null;
            item._playerInventory = null;
            _currentItem = null; //"_currentItem" va a ser igual a null.
            _currentItemData = null;
        }
    }

    public void SetUsingItem(bool state) 
    {
        _usingItem = state;
    }
    public bool GetUsingItem()
    {
       return _usingItem;
    }

    public bool GetItemSaved()//Metodo GET para enviar el valor de "_inInventory"
    {
        return _inRange;
    }

    public void OnAttack(InputValue value) 
    {
        if (value.isPressed && attacking == false) 
        {
            _weaponMelee.SetActive(true);
        }
    }

  /*  public GameObject _itemPrefab;
    private Transform _transformPart;
    private Transform _playerT;

    [ServerRpc]
    public void EquipItemServerRpc(uint itemId)
    {
        foreach (var networkPrefab in NetworkManager.Singleton.NetworkConfig.Prefabs.Prefabs) 
        {
            if (itemId == networkPrefab.SourcePrefabGlobalObjectIdHash)
            {
                _itemPrefab = networkPrefab.Prefab;
                GameObject itemEquiped = Instantiate(networkPrefab.Prefab, _transformPart);
                itemEquiped.GetComponent<NetworkObject>().Spawn();
                itemEquiped.GetComponent<NetworkObject>().TrySetParent(_playerT);
            }
        }
    }

    public void EquipItem(uint itemId, Transform transformPart, Transform playerT) 
    {
        
        _transformPart = transformPart;
        _playerT = playerT;

        EquipItemServerRpc(itemId);
    }*/

    //MEJORA DE ATRIBUTOS
    public void UpgradeAttributes(string nameAttribute, int newValue) 
    {
        if (_attributes.ContainsKey(nameAttribute)) 
        {
            _attributes[nameAttribute] += newValue;
        }
    }

    private void Update()
    {
        foreach (KeyValuePair<string, int> at in _attributes) 
        {
            Debug.Log($"ATRIBUTO: {at.Key} Y SU VALOR ES DE: {at.Value}");
        }
        //if (!IsOwner) return;

        Vector3 moveDirection = GetCameraRelativeDirection();

        RotateCharacter(moveDirection);
        moveDirection = ApplyGravity(moveDirection);
        MoveCharacter(moveDirection);

        if (attacking == true) 
        {
            _weaponMelee.SetActive(false);
            attacking = false;
        }
    }
}