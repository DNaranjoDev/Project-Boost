using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor; // Lo convierte en un slider desde el valor 0 hasta el 1 en el inspector
    [SerializeField] float period = 3f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } // Para comparar un float value es preferible hacerlo así ya que un float puede variar mucho y el valor más cercano a 0 para Unity es Mathf.Epsilon
        float cycles = Time.time / period; // Aumenta durante el tiempo
        const float tau = Mathf.PI * 2; // Valor constante de 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // Transcurre desde -1 a 1

        movementFactor = (rawSinWave + 1f) / 2f; // Se recalcula para que vaya de 0 a 1 para que quede más limpio

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
