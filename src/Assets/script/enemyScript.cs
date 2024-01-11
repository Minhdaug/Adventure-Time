using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.script.model;

public class NPCMovement : MonoBehaviour
{
    public Vector3[] waypoints;   // Array of positions the NPC will follow
    public float moveSpeed = 3f;  // Adjust this value to control the NPC's movement speed
    public EnemyName enemyName;

    private int currentWaypointIndex = 0;

    void Start()
    {
        delayAppearance();
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned to NPCMovement script on " + gameObject.name);
            enabled = false; // Disable the script if no waypoints are assigned
        }
    }

    void Update()
    {
        MoveNPC();
    }

    void MoveNPC()
    {
        if (waypoints.Length == 0)
        {
            return; // Exit the function if there are no waypoints
        }

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], moveSpeed * Time.deltaTime);

        // Check if the NPC has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            SceneManager.LoadScene("Combat");
        }

    }

    async Task delayAppearance ()
    {
        gameObject.SetActive(false);
        await Task.Delay(1000);
        gameObject.SetActive(true);
    }
}