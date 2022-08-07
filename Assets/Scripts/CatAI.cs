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
    [SerializeField] private Animator mAnimator;
    [SerializeField] private Cat mCat;
    [SerializeField] private SpriteRenderer mSprite;
    [SerializeField] private SpawnArea area;
    [SerializeField] private float speed = 1;

    private CatState currentState;
    private float stateTimer;
    private Vector3 targetLocation;
    private bool isSelected;
    private bool isAngry;
    private int prevRelationship;



    void Update()
    {
        if (!mCat.IsCatActive() || GameManager.Instance.IsMinigameActive) return;

        if (isSelected || isAngry)
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
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, Time.deltaTime * speed);
                if (transform.position.x < targetLocation.x)
                {
                    mSprite.flipX = true;
                }
                else
                {
                    mSprite.flipX = false;
                }
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

    private void OnEnable()
    {
        mCat.OnRelationshipUpdated += CheckStats;
    }

    void OnDisable()
    {
        mCat.OnRelationshipUpdated -= CheckStats;
    }


    private void CheckStats(int newRelationship)
    {
        if (newRelationship < prevRelationship || newRelationship == 0)
        {
            mAnimator.SetBool("IsAngry", true);
            isAngry = true;
        }
        else
        {
            mAnimator.SetBool("IsAngry", false);
            isAngry = false;
        }
        prevRelationship = newRelationship;
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
            stateTimer = Random.Range(1, 10);
        }
        else if (currentState == CatState.walk)
        {
            mAnimator.SetBool("IsMoving", true);
            targetLocation = RetrieveRandomPosition();
        }
        else if (currentState == CatState.pose1)
        {
            mAnimator.SetBool("IsPose1", true);
            stateTimer = Random.Range(3, 8);
        }
        else if (currentState == CatState.pose2)
        {
            mAnimator.SetBool("IsPose2", true);
            stateTimer = Random.Range(3, 8);
        }
        else if (currentState == CatState.loaf)
        {
            mAnimator.SetBool("IsLoafing", true);
            stateTimer = Random.Range(3, 8);
        }
        else
        {
            print("An invalid State has been selected");
            currentState = CatState.idle;
        }
    }


    private Vector2 RetrieveRandomPosition()
    {
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

    public void PlayWin()
    {
        mAnimator.SetBool("IsSelected", false);
        isSelected = false;
        mAnimator.SetBool("Win", true);
    }

    public void PlayLose()
    {
        mAnimator.SetBool("IsSelected", false);
        isSelected = false;
        mAnimator.SetBool("Lose", true);
    }

    public SpriteRenderer GetSprite()
    {
        return mSprite;
    }
}

