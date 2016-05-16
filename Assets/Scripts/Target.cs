using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReactToHit()
    {
        MovingAI behavior = GetComponent<MovingAI>();
        if (behavior)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        //research tweens for a smoove (motion) transform
        this.transform.Rotate(75, 0, 0);
   
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
