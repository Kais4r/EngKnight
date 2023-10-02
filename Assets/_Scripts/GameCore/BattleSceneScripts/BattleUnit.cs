using UnityEngine;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] bool isPlayerUnit;
    [SerializeField] bool isBossUnit;

    public float characterEnterSpeed = 1.0f;

    Vector3 originalPos;
    Renderer objectRenderer;
    Color originalColor;
    Animator animator;

    public void Setup()
    {
        originalPos = this.gameObject.transform.localPosition;
        objectRenderer = this.gameObject.GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        if (isPlayerUnit || isBossUnit)
        {
            animator = this.gameObject.GetComponent<Animator>();
        }
        PlayEnterAnimation();
    }

    public void PlayEnterAnimation()
    {
        float moveDistance = 10f;
        float duration = 1f;

        if (isPlayerUnit)
        {
            this.gameObject.transform.DOLocalMoveX(originalPos.x + moveDistance, duration);
            originalPos.x += moveDistance;
        }
        else
        {
            this.gameObject.transform.DOLocalMoveX(originalPos.x - moveDistance, duration);
            originalPos.x -= moveDistance;
        }
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if (isPlayerUnit)
        {
            sequence.Append(this.gameObject.transform.DOLocalMoveX(originalPos.x + 1f, 0.25f));
            animator.SetTrigger("isAttacking");
        }
        else
        {
            sequence.Append(this.gameObject.transform.DOLocalMoveX(originalPos.x - 1f, 0.25f));
            if (isBossUnit)
            {
                animator.SetTrigger("isAttacking");
            }
        }
        sequence.Append(this.gameObject.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayHitAnimation()
    {

        var sequence = DOTween.Sequence();
        sequence.Append(objectRenderer.material.DOColor(Color.red, 0.2f));
        sequence.Append(objectRenderer.material.DOColor(originalColor, 0.2f));
    }
}