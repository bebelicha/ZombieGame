using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
   private int quantidadeDeCura = 20;
   private int tempoDestruir = 5;
    void Start()
    {
        Destroy(gameObject, tempoDestruir);
    }
    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if(objetoDeColisao.tag == "jogador")
        {
            objetoDeColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
            Destroy(gameObject);
        }
    }
}
