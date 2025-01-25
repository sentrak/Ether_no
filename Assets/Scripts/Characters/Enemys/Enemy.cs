using System;
using UnityEngine;

/*
 * Clase: Enemy.
 * Descripción: Define las propiedades básicas de un enemigo, incluyendo su nombre, puntos de vida, velocidad de movimiento y su vida máxima.
 */
public class Enemy : MonoBehaviour
{
    [Header("Basic Properties")]
    public string enemyName; // Nombre del enemigo
    public int healtPoints; // Puntos de vida actuales del enemigo
    public int MaxHealtPoints; // Puntos de vida máximos del enemigo

    [Header("Movement Settings")]
    public int moveSpeed; // Velocidad de movimiento del enemigo
}
