using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFadeOut : MonoBehaviour
{
    public GameObject sourceObject; // The object whose material settings will be copied
    public Material targetMaterial; // The target material
    private Renderer meshRenderer = default;
    private Color newColor = default;

    [SerializeField] private float waitBeforeFade = 0f;
    [SerializeField] private float fadeDelay = 2f;
    [SerializeField] private float currentAlpha = 1;
    [SerializeField] private float requiredAlpha = 0;

    void Start()
    {
        if (targetMaterial == null)
        {
            Debug.LogError("Target material is not assigned.");
            return;
        }

        CopyMaterialSettings();

        try
        {
            meshRenderer = GetComponentInChildren<Renderer>();
            newColor = meshRenderer.material.color;
            newColor.a = currentAlpha;
            StartCoroutine(FadeObject(currentAlpha, requiredAlpha, fadeDelay));
        }
        catch (System.Exception) { Debug.Log("No renderer found."); }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator FadeObject(float currentAlpha, float requiredAlpha, float fadeTime)
    {
        yield return new WaitForSeconds(waitBeforeFade);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {
            newColor.a = Mathf.Lerp(currentAlpha, requiredAlpha, t);
            meshRenderer.material.color = newColor;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    void CopyMaterialSettings()
    {
        if (sourceObject == null)
        {
            Debug.LogError("Source object is not assigned.");
            return;
        }

        Renderer[] sourceRenderers = sourceObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in sourceRenderers)
        {
            Material[] sourceMaterials = renderer.sharedMaterials;
            Material[] newMaterials = new Material[sourceMaterials.Length];

            for (int i = 0; i < sourceMaterials.Length; i++)
            {
                Material sourceMaterial = sourceMaterials[i];
                Material newMaterial = new Material(targetMaterial); // Create a new instance of the target material

                // Copy all material properties
                newMaterial.CopyPropertiesFromMaterial(sourceMaterial);

                newMaterials[i] = newMaterial;
            }

            renderer.materials = newMaterials; // Assign the new materials to the renderer
        }
    }
}