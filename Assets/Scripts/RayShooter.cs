using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour {

    private Camera _camera;
    private static readonly float _destroyAfterXSeconds = 1.0f;
    private static readonly int _cursorSize = 12;

    // Use this for initialization
    void Start () { 
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            var point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                Target target = hitObject.GetComponent<Target>();
                if (target != null) {
                    target.ReactToHit();
                }
                else {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
	}

    void OnGUI()
    {
        float posX = _camera.pixelWidth / 2 - _cursorSize / 2;
        float posY = _camera.pixelHeight / 2 - _cursorSize / 2;
        GUI.Label(new Rect(posX, posY, _cursorSize, _cursorSize), "*");
    }

    private IEnumerator SphereIndicator(Vector3 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;

        yield return new WaitForSeconds(_destroyAfterXSeconds);

        Destroy(sphere);
    }
}
