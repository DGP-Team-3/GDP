using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatState
{
    idle,
    walk,
    pose1,
    pose2,
    loaf,
    COUNT
}
public class CatAI : MonoBehaviour
{
    private float stateTimer;
    private Animator mAnimator;
    private CatState currentState;
    private Vector3 targetLocation;
    private bool isSelected;
    [SerializeField] private float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {

        }
        else
        {
            stateTimer -= Time.deltaTime;
            if (currentState == CatState.idle)
            {
                if (stateTimer <= 0.0f)
                {
                    FindNewState();
                }
            }
            else if (currentState == CatState.walk)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetLocation, Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, targetLocation) < 0.001f)
                {
                    FindNewState();
                }
            }
            else if (currentState == CatState.pose1)
            {
                if (stateTimer <= 0.0f)
                {
                    FindNewState();
                }
            }
            else if (currentState == CatState.pose2)
            {
                if (stateTimer <= 0.0f)
                {
                    FindNewState();
                }
            }
            else if (currentState == CatState.loaf)
            {
                if (stateTimer <= 0.0f)
                {
                    FindNewState();
                }
            }
            else
            {
                print("Cat is in an invalid State");
            }
        }
    }

    private void FindNewState()
    {
        mAnimator.SetBool("IsMoving", false);
        mAnimator.SetBool("IsSpecial", false);
        mAnimator.SetBool("IsPose1", false);
        mAnimator.SetBool("IsPose2", false);
        mAnimator.SetBool("IsLoafing", false);
        currentState = (CatState)Random.Range(0, (int)CatState.COUNT);
        if (currentState == CatState.idle)
        {
            stateTimer = Random.Range(1, 5);
        }
        else if (currentState == CatState.walk)
        {
            mAnimator.SetBool("IsMoving", true);
            targetLocation = RetrieveRandomPosition();
        }
        else if (currentState == CatState.pose1)
        {
            mAnimator.SetBool("IsPose1", true);
            stateTimer = Random.Range(1, 5);
        }
        else if (currentState == CatState.pose2)
        {
            mAnimator.SetBool("IsPose2", true);
            stateTimer = Random.Range(1, 5);
        }
        else if (currentState == CatState.loaf)
        {
            mAnimator.SetBool("IsLoafing", true);
            stateTimer = Random.Range(1, 5);
        }
        else
        {
            print("An invalid State has been selected");
            currentState = CatState.idle;
        }
    }
    private Vector2 RetrieveRandomPosition()
    {
        SpawnArea area = FindObjectOfType<SpawnArea>();

        float xPos = Random.Range(area.XMinSpawn, area.XMaxSpawn);
        float yPos = Random.Range(area.YMinSpawn, area.YMaxSpawn);
        return new Vector2(xPos, yPos);
    }
    
    public void selectCat()
    {
        mAnimator.SetBool("IsSelected", true);
        isSelected = true;
    }

    public void deselectCat()
    {
        mAnimator.SetBool("IsSelected", false);
        isSelected = false;
    }
}
