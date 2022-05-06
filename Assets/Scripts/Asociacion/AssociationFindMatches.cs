using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssociationFindMatches : MonoBehaviour
{
    private AssociationBoard board;
    public List<GameObject> currentMatches = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<AssociationBoard>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentDot = board.allDots[i, j];
                if (currentDot != null)
                {
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];
                        if (leftDot != null && rightDot != null)
                        {
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {
                                if (!currentMatches.Contains(leftDot)) currentMatches.Add(leftDot);
                                if (!currentMatches.Contains(rightDot)) currentMatches.Add(rightDot);
                                if (!currentMatches.Contains(currentDot)) currentMatches.Add(currentDot);
                                leftDot.GetComponent<AssociationDot>().isMatched = true;
                                rightDot.GetComponent<AssociationDot>().isMatched = true;
                                currentDot.GetComponent<AssociationDot>().isMatched = true;
                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject downDot = board.allDots[i, j - 1];
                        GameObject upDot = board.allDots[i, j + 1];
                        if (downDot != null && upDot != null)
                        {
                            if (downDot.tag == currentDot.tag && upDot.tag == currentDot.tag)
                            {
                                if (!currentMatches.Contains(downDot)) currentMatches.Add(downDot);
                                if (!currentMatches.Contains(upDot)) currentMatches.Add(upDot);
                                if (!currentMatches.Contains(currentDot)) currentMatches.Add(currentDot);
                                downDot.GetComponent<AssociationDot>().isMatched = true;
                                upDot.GetComponent<AssociationDot>().isMatched = true;
                                currentDot.GetComponent<AssociationDot>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
