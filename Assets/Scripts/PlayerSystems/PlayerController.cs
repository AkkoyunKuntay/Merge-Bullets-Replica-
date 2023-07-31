using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class PlayerController : CustomSingleton<PlayerController>
{
    [Header("Current Buffer Settings")]
    public float fireRate;
    public float range;
    public float bulletSize;

    private bool isAllowedToMove;
    private Vector3 defaultPos;
    [SerializeField] bool hasFullGun;

    public List<PlayerNode> nodes = new List<PlayerNode>();
    public List<Gun> handedGuns = new List<Gun>();

    public event System.Action GateBuffersTakenEvent;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
        LevelManager.instance.ShootingIsOnEvent += OnShootingIsOn;
    }

    private void OnShootingIsOn()
    {
        if (!HasAvailableGun()) GameManager.instance.EndGame(false);
    }

    public void RegisterGun(Gun desiredGun)
    {
        if (!handedGuns.Contains(desiredGun))
        {
            handedGuns.Add(desiredGun);
        }
    }
    public bool HasAvailableGun()
    {
        return handedGuns.Count > 0;
    }
    public PlayerNode FindAvailableNode()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (!nodes[i].HasGun()) return nodes[i];
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseGate gate))
        {
            if(gate.gateType == BaseGate.GateTypes.PositiveFireRate || gate.gateType == BaseGate.GateTypes.NegativeFireRate)
            {
                Debug.Log("Gate Collision");
                fireRate -= gate.GiveGateBufferToGun()/10;
                GateBuffersTakenEvent?.Invoke();
            }
            if (gate.gateType == BaseGate.GateTypes.PositiveRange || gate.gateType == BaseGate.GateTypes.NegativeRange)
            {
                Debug.Log("Gate Collision");
                range += gate.GiveGateBufferToGun();
                if (range <= 0)
                {
                    range = 1f;
                }
                GateBuffersTakenEvent?.Invoke();
            }
        }

    }

}
