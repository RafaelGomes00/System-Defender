using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public Rigidbody fisica;
    public float velocidade;
    public float forcaDano = 10;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Inimigo"))
        {
            other.GetComponent<Inimigo>().CausaDano(forcaDano);
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 10);
    }
    private void Movimenta()
    {
        fisica.velocity = this.transform.forward * velocidade;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Movimenta();
    }
}
