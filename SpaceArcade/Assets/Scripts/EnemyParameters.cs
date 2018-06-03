using UnityEngine;

[CreateAssetMenu()]
public class EnemyParameters : ScriptableObject
{

    public float _moveSpeed=5f;
    public float _bulletSpeed = -10f;
    public float _fireRate = 0.5f;
    public int _scoreToDie = 10;
    public int _shieldPower = 0;
    public Sprite[] _ships;
    public LayerMask layerMask;
    //public ShipColor shipcolor = ShipColor.black;

    //public enum ShipColor { black, blue, green, red }
}
