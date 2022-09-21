using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject Camara1;
    public GameObject Camara2;
    public GameObject Camara3;
    public GameObject Camara4;

    public void CamaraTV(){

        Camara2.SetActive(true);
        Camara1.SetActive(false);
        Camara3.SetActive(false);
        Camara4.SetActive(false);
    }
    public void Camaranormal(){

        Camara1.SetActive(true);
        Camara2.SetActive(false);
        Camara3.SetActive(false);
        Camara4.SetActive(false);
    }
    public void CamaraLSD(){

        Camara3.SetActive(true);
        Camara1.SetActive(false);
        Camara2.SetActive(false);
        Camara4.SetActive(false);
    }
    public void CamaraCubo(){

        Camara4.SetActive(true);
        Camara1.SetActive(false);
        Camara3.SetActive(false);
        Camara2.SetActive(false);
    }
}
