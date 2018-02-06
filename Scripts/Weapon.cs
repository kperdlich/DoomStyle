using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    [SerializeField]
    private Sprite[] _reloadSpriteAnimation;

    [SerializeField]
    private Sprite _weaponSprite;

    [SerializeField]
    private AudioClip _shotSound;    

    private bool _shooting = false, _animationRunning = false;
    private Coroutine _coroutine;    
    
    void Start ()
    {
        GetComponent<SpriteRenderer>().sprite = _weaponSprite;
    }

    void OnGUI()
    {   
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10, 10), "");
    }
	
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_shooting)
        {
            Shoot();            
        }
    }

    protected virtual void Shoot()
    {
        GetComponent<AudioSource>().clip = _shotSound;
        GetComponent<AudioSource>().Play();
        _shooting = true;
        if (_animationRunning)
        {
            StopCoroutine(_coroutine);
            _animationRunning = false;
        }
        _coroutine = StartCoroutine(PlayShootAnimation());

        GameObject[] bullets = new GameObject[6];
        for (int i = 0; i < 6; ++i)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), .0f) + (transform.forward*1.5f);
            go.transform.rotation = transform.rotation;
            go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            var light = go.AddComponent<Light>();
            light.type = LightType.Point;
            go.AddComponent<Rigidbody>();
            bullets[i] = go;
        }

        foreach (var b in bullets)
        {
            var r = b.GetComponent<Rigidbody>();
            r.AddForce(transform.forward * 100.0f, ForceMode.VelocityChange);
            Destroy(b, 5.0f);
        }
    }


    protected virtual IEnumerator PlayShootAnimation()
    {        
        for (int i = 0; i < _reloadSpriteAnimation.Length; ++i)
        {
            GetComponent<SpriteRenderer>().sprite = _reloadSpriteAnimation[i];

            if (i == Mathf.CeilToInt(_reloadSpriteAnimation.Length * 0.5f))
                _shooting = false;

            if (i < _reloadSpriteAnimation.Length -1)
                yield return new WaitForSeconds(0.1f);
        }
        _animationRunning = false;
    }
}
