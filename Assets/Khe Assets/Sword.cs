using UnityEngine;

/// <summary>
/// A class for controlling when the sword is lethal, and for dealing dmg
/// </summary>
public class Sword : MonoBehaviour
{
    public PlayerController player;
    public GameObject edge;



    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        if (player == null)
        {
            player = GetComponentInParent<PlayerController>();
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
 
        if (player.attacking)
        {
            edge.GetComponent<Collider>().enabled = true;
        }
        else
        {
            edge.GetComponent<Collider>().enabled = false;
        }
    }
}