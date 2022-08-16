using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform segmentPrefab;
    [SerializeField] int initialSize = 4;
    private Vector2 moveDirection = Vector2.right;
    List<Transform> segments;

    SwipeManager swipeManager;

    private void Awake()
    {
        swipeManager = SwipeManager.Instance;
        segments = new List<Transform>();
        RestartGame();
    }

    private void Update()
    {
        switch (swipeManager.Direction)
        {
            case SwipeManager.SwipeDirection.UP:
                if (moveDirection == Vector2.down)
                    return;
                moveDirection = Vector2.up;
                break;
            case SwipeManager.SwipeDirection.RIGHT:
                if (moveDirection == Vector2.left)
                    return;
                moveDirection = Vector2.right;
                break;
            case SwipeManager.SwipeDirection.DOWN:
                if (moveDirection == Vector2.up)
                    return;
                moveDirection = Vector2.down;
                break;
            case SwipeManager.SwipeDirection.LEFT:
                if (moveDirection == Vector2.right)
                    return;
                moveDirection = Vector2.left;
                break;
        }
    }

    private void FixedUpdate()
    {
        for(int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        transform.position = new Vector3(
            Mathf.Round(transform.position.x + moveDirection.x),
            Mathf.Round(transform.position.y + moveDirection.y),
            0);
    }

    private void Grow()
    {
        Transform newSegment = Instantiate(segmentPrefab);
        newSegment.position = segments[segments.Count - 1].position;

        segments.Add(newSegment);
    }

    void RestartGame()
    {
        for(int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(transform);

        for(int i = 1; i < initialSize; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
        }

        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var food = other.GetComponent<FoodManager>();
        if (food != null)
        {
            Grow();
        }
        else if(other.tag == "Obstacle")
        {
            RestartGame();
        }
    }
}
