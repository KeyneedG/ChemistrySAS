using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistryManager : MonoBehaviour
{
    public Transform[] components;
    private Vector3[] originalPoses; //Original local position of chemical components to return them back when should be reseted.
    public Transform[] finalComponents; //Final chemical components transform, super primitive and unscalable but pretty reliable.

    public MeshRenderer checkIfVisibleH2;
    public MeshRenderer checkIfVisibleO2;

    public Transform model1, model2, modelBetween;

    public GameObject text1, text2, textBetween;

    private bool isReacted;
    private bool isAnimIsDone;

    private void Awake()
    {
        originalPoses = new Vector3[components.Length];
        for (int i = 0; i < components.Length; i++)
        {
            originalPoses[i] = components[i].localPosition;
        }
    }

    private void Update()
    {
        UpdateOnReacted();

        if (checkIfVisibleH2.enabled && checkIfVisibleO2.enabled && Vector3.Distance(model1.position, model2.position) < .25)
        {
            if (!isReacted)
            {
                isReacted = true;
                UpdateOnReacted();
                DoReaction();
            }
        }
        else
        {
            if (isReacted)
            {
                ResetComponents();
                isReacted = false;
                isAnimIsDone = false;
            }
        }
    }

    public void UpdateOnReacted()
    {
        if (isReacted)
        {
            modelBetween.position = (model2.position + model1.position) / 2f;

            if (isAnimIsDone)
            {
                for (int i = 0; i < components.Length; i++)
                {
                    components[i].position = finalComponents[i].position;
                }
            }
        }
    }

    public void ResetComponents()
    {
        t.Stop();

        for (int i = 0; i < components.Length; i++)
        {
            components[i].localPosition = originalPoses[i];
        }

        text1.SetActive(true);
        text2.SetActive(true);
        textBetween.SetActive(false);
    }

    private PrimeTween.Tween t;

    public void DoReaction()
    {
        ResetComponents();

        for (int i = 0; i < components.Length; i++)
        {
            t = PrimeTween.Tween.Position(components[i], finalComponents[i].position, 1f, PrimeTween.Ease.InOutBack).OnComplete(() => { isAnimIsDone = true; });
        }

        text1.SetActive(false);
        text2.SetActive(false);
        textBetween.SetActive(true);
    }
}
