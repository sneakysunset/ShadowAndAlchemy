using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerFireProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float projectileSpeed = 10f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0; // Assure-toi que la position Z est correcte pour 2D

        Vector3 direction = (mousePosition - spawnPoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }
}