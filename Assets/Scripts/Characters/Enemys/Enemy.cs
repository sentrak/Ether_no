using System;
using UnityEngine;

/*
 * Clase: Enemy.
 * Descripción: Define las propiedades básicas de un enemigo, incluyendo su nombre, puntos de vida y velocidad de movimiento.
 */
public class Enemy : MonoBehaviour
{
    public String enemyName; // Nombre del enemigo
    public int healtPoints; // Puntos de vida del enemigo
    public int MaxHealtPoints; // Puntos de vida del enemigo
    public int moveSpeed; // Velocidad de movimiento del enemigo
}