using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    public LayerMask TargetLayerMask { get; set; }
    public Vector3 Dir { get; set; }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(Dir * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(LayerMaskUtil.ContainsLayer(TargetLayerMask, collision.gameObject))
        {
            Debug.Log("shoot");
            Destroy(gameObject);
        }
    }
}
