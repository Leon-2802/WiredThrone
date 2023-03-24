using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagePositions : MonoBehaviour
{
    public static ManagePositions instance;
    public List<Vector3> robotPositions = new List<Vector3>();
    public List<Vector3> enemyPositions = new List<Vector3>();
    [SerializeField] private float moveSpeed = 0.5f;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
    }

    public bool EnemyOnBlock(Vector2 position)
    {
        foreach (Vector2 v2 in enemyPositions)
        {
            if (v2 == position)
                return true;
        }

        return false;
    }

    public void SetTranslationTarget(GameObject obj, int robotIndex, int enemyIndex)
    {
        if (obj.GetComponent<Robot>())
        {
            Vector2 translation = new Vector2(enemyPositions[enemyIndex].x - robotPositions[robotIndex].x,
                enemyPositions[enemyIndex].y - robotPositions[robotIndex].y);
            TranslatePosition(obj, translation);
        }
    }

    public void TranslatePosition(GameObject obj, Vector2 translation)
    {
        StartCoroutine(TranslationSteps(obj, translation));
    }

    IEnumerator TranslationSteps(GameObject obj, Vector2 translation)
    {
        if (translation.x > 0)
        {
            for (int i = 1; i < translation.x; i++)
            {
                yield return new WaitForSeconds(moveSpeed);
                Move(obj, new Vector2(1, 0));
            }
        }
        else
        {
            for (int i = -1; i > translation.x; i--)
            {
                yield return new WaitForSeconds(moveSpeed);
                Move(obj, new Vector2(-1, 0));
            }
        }


        if (translation.y > 0)
        {
            for (int i = 1; i < translation.y; i++)
            {
                yield return new WaitForSeconds(moveSpeed);
                Move(obj, new Vector2(0, 1));
            }
        }
        else
        {
            for (int i = -1; i > translation.y; i--)
            {
                yield return new WaitForSeconds(moveSpeed);
                Move(obj, new Vector2(0, -1));
            }
        }
    }

    private void Move(GameObject obj, Vector2 translation)
    {
        obj.transform.position = new Vector3(obj.transform.position.x + translation.x, obj.transform.position.y,
            obj.transform.position.z + translation.y);
    }
}
