using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MeshFadeOut : MonoBehaviour
{
    private Material targetMaterial; // The target material
    private Renderer meshRenderer = default;
    private Color newColor = default;

    [SerializeField] public float waitBeforeFade = 2.0f;
    [SerializeField] public float fadeDelay = 3f;
    [SerializeField] private float currentAlpha = 1;
    [SerializeField] private float requiredAlpha = 0;
    void Start()
    {
        targetMaterial = Resources.Load<Material>("Transparent");
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
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        yield return new WaitForSeconds(waitBeforeFade);
        
        // Get all renderers of the object and its children
    
        // Fade out each material of each renderer simultaneously
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {
            // Interpolate alpha value for each material of each renderer
            foreach (Renderer renderer in renderers)
            {
                Material[] materials = renderer.materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    Color color = materials[i].color;
                    color.a = Mathf.Lerp(currentAlpha, requiredAlpha, t);
                    materials[i].color = color;
                }
            }
    
            yield return null;
        }
        Destroy(gameObject);
    }



void CopyMaterialSettings()
{
    if (this.gameObject == null)
    {
        Debug.LogError("Source object is not assigned.");
        return;
    }

    Renderer[] sourceRenderers = this.gameObject.GetComponentsInChildren<Renderer>();

    foreach (Renderer renderer in sourceRenderers)
    {
        Material[] sourceMaterials = renderer.sharedMaterials;
        Material[] newMaterials = new Material[sourceMaterials.Length];

        for (int i = 0; i < sourceMaterials.Length; i++)
        {
            Material sourceMaterial = sourceMaterials[i];
            Material newMaterial = new Material(targetMaterial); // Create a new instance of the target material

            // Set color property with desired color
            if (sourceMaterial.HasProperty("_Color"))
            {
                Color sourceColor = sourceMaterial.color;
                Color newColor = new Color(sourceColor.r, sourceColor.g, sourceColor.b, sourceColor.a);
                newMaterial.color = newColor;
            }
            else
            {
                Debug.LogWarning("Source material " + sourceMaterial.name + " does not have a color property.");
            }


            if (sourceMaterial.HasProperty("_EmissionColor"))
            {
                newMaterial.SetColor("_EmissionColor", sourceMaterial.GetColor("_EmissionColor"));
                newMaterial.EnableKeyword("_EMISSION");
            }
            else
            {
                newMaterial.EnableKeyword("_EMISSION");
                newMaterial.SetColor("_EmissionColor", Color.black);
                newMaterial.DisableKeyword("_EMISSION");
            }


            if (sourceMaterial.mainTexture != null)
            {
                newMaterial.mainTexture = sourceMaterial.mainTexture;
            }


            // if (sourceMaterial.HasProperty("_SpecularHighlights") && sourceMaterial.GetFloat("_SpecularHighlights") == 1)
            // {
            //     // Do something if specular highlights are enabled
            //     Debug.Log("Specular highlights are enabled.");
            // }


            if (sourceMaterial.HasProperty("_Mode"))
            {
                if (sourceMaterial.GetFloat("_Mode") == 2) // Assuming transparent rendering mode is set to "Fade"
                {
                    newMaterial.SetFloat("_Mode", 2); // Set rendering mode to "Fade"
                    newMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    newMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    newMaterial.SetInt("_ZWrite", 0);
                    newMaterial.DisableKeyword("_ALPHATEST_ON");
                    newMaterial.EnableKeyword("_ALPHABLEND_ON");
                    newMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    newMaterial.renderQueue = 3000;
                }
            }

            newMaterials[i] = newMaterial;
        }

        renderer.materials = newMaterials; // Assign the new materials to the renderer
    }
}

    


}