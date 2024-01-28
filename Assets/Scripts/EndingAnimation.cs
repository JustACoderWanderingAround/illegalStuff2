using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAnimation : MonoBehaviour
{
    public Transform walk;
    public SpriteRenderer sr;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(endingAni());
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, walk.position, Time.deltaTime * 2.5f);
    }

    // Update is called once per frame
    public IEnumerator endingAni()
    {
        yield return new WaitForSeconds(1.5f);
        sr.sprite = sprites[1];
        yield return new WaitForSeconds(1f);
        sr.sprite = sprites[2];
        yield return new WaitForSeconds(1f);
        sr.sprite = sprites[3];
        yield return new WaitForSeconds(2f);
        sr.sprite = sprites[4];
    }
}
